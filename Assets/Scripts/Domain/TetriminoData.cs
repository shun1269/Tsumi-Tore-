using UnityEngine;

[CreateAssetMenu(fileName = "TetrominoData", menuName = "DPC/Tetromino Data")]
public class TetriminoData : ScriptableObject
{
    public MinoType Type; 

    // 各向きのブロック座標
    public Vector2Int[] North_Offsets;
    public Vector2Int[] East_Offsets;
    public Vector2Int[] South_Offsets;
    public Vector2Int[] West_Offsets;

    /// <summary>
    /// 指定した回転状態に対応するオフセットを取得
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns>対応する形状の座標配列</returns>
    public Vector2Int[] GetOffsets(RotationState rotation)
    {
        switch (rotation)
        {
            case RotationState.North: return North_Offsets;
            case RotationState.East:  return East_Offsets;
            case RotationState.South: return South_Offsets;
            case RotationState.West:  return West_Offsets;
            default: return North_Offsets; // 念のため
        }
    }
}