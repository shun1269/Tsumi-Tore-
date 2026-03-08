using UnityEngine;
using VContainer;

public class RotateUseCase
{
    private readonly Field _field;
    private readonly SRSKickData _kickData;

    [Inject]
    public RotateUseCase(Field field, SRSKickData kickData)
    {
        _field = field;
        _kickData = kickData;
    }

    public bool TryRotate(Tetrimino mino, bool isClockwise)
    {
        // 今の向きと回転後の向きを取得
        RotationState currentRot = mino.Rotation;
        RotationState newRot = GetNextRotation(currentRot, isClockwise);

        // Oミノは形状が変わらないため、その場で回転状態だけ更新する
        if (mino.Data.Type == MinoType.O)
        {
            if (!IsValidPosition(mino, mino.Position, newRot))
            {
                return false;
            }

            mino.Rotation = newRot;
            return true;
        }

        // 使うべきキックルールを選ぶ
        KickTest[] kickTests = (mino.Data.Type == MinoType.I) 
            ? _kickData.I_MinoKicks 
            : _kickData.StandardKicks;

        // 今回の回転に対応するパターンを探す
        KickTest testCase = null;
        foreach (var test in kickTests)
        {
            // 回転前と回転後の向きが一致するテストケースを探す
            if(test.fromRotation == currentRot && test.toRotation == newRot)
            {
                testCase = test;
                break;
            }
        }

        if (testCase == null)
        {
            return false;
        }

        foreach (var kickOffset in testCase.kickOffsets)
        {
            Vector2Int candidatePosition = mino.Position + kickOffset;

            if (!IsValidPosition(mino, candidatePosition, newRot))
            {
                continue;
            }

            mino.Position = candidatePosition;
            mino.Rotation = newRot;
            return true;
        }

        return false;
    }

    private RotationState GetNextRotation(RotationState current, bool isClockwise)
    {
        if (isClockwise)
        {
            return (RotationState)(((int)current + 1) % 4);
        }
        else
        {
            return (RotationState)(((int)current + 3) % 4); // 反時計回りは時計回りの逆
        }
    }

    private bool IsValidPosition(Tetrimino mino, Vector2Int position, RotationState rotation)
    {
        foreach (var offset in mino.Data.GetOffsets(rotation))
        {
            Vector2Int blockPos = position + offset;

            if (_field.IsCellOccupied(blockPos))
            {
                return false;
            }
        }

        return true;
    }
        
}