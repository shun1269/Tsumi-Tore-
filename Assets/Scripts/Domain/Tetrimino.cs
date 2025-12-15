using UnityEngine;

public class Tetrimino
{
    public MinoType Type { get; set; }
    public RotationState Rotation { get; set; }
    public Vector2Int Position { get; set; }
    public TetriminoData Data { get; set; }
    public Tetrimino(TetriminoData data)
    {
        Data = data;
        Type = data.Type;
        Rotation = RotationState.North;
        Position = new Vector2Int(4, 21);
    }
    // 回転や移動のロジックはUse Caseで実装
}