using UnityEngine;
using VContainer;

public class MoveUseCase
{
    private readonly Field _field;

    [Inject]
    public MoveUseCase(Field field)
    {
        _field = field;
    }

    public bool tryMove(Tetrimino mino, Vector2Int direction)
    {
        Vector2Int newPosition = mino.Position + direction;
        
        if(IsValidPosition(mino, newPosition, mino.Rotation))
        {
            mino.Position = newPosition;
            return true;
        }
        return false;
    }

    private bool IsValidPosition(Tetrimino mino, Vector2Int position, RotationState rotation)
    {
        // ミノの各ブロックの位置をチェック
        foreach (var offset in mino.Data.GetOffsets(rotation))
        {
            Vector2Int blockPos = position + offset;

            // フィールド外または他のミノと衝突している場合は無効
            if (!_field.IsCellOccupied(blockPos))
            {
                return false;
            }
        }
        return true;
    }
}