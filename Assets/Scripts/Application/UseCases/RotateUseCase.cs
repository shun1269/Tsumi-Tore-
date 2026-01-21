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

    public bool TryRoatate(Tetrimino mino, bool isClockwise)
    {
        // 今の向きと回転後の向きを取得
        RotationState currentRot = mino.Rotation;
        RotationState newRot = GetNextRotation(currentRot, isClockwise);

        // 使うべきキックルールを選ぶ
        KickTest[] tests = (mino.Data.Type == MinoType.I) 
            ? _kickData.I_MinoKicks 
            : _kickData.StandardKicks;

        // 今回の回転に対応するパターンを探す
        KickTest testCase = null;
        foreach (var t in tests)
        {
            if(t.fromRotation == currentRot && t.toRotation == newRot)
            {
                testCase = t;
                break;
            }

        }

    }
        
}