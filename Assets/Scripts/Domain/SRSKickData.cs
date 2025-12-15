using UnityEditor.EditorTools;
using UnityEngine;

[System.Serializable]
public class KickTest
{
    public RotationState fromRotation;
    public RotationState toRotation;
    [Tooltip("回転テスト時のオフセット")]
    public Vector2Int[] kickOffsets;
}

[CreateAssetMenu(fileName = "SRSKickData", menuName = "DPC/SRS Kick Data")]
public class SRSKickData : ScriptableObject
{
    public KickTest[] StandardKicks; // T,S,Z,L,Jミノ用の回転テスト

    public KickTest[] I_MinoKicks; // Iミノ用の回転テスト
}