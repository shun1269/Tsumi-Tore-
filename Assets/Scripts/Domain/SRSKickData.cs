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

    private void OnEnable()
    {
        EnsureDefaultTables();
    }

    private void OnValidate()
    {
        EnsureDefaultTables();
    }

    [ContextMenu("Load Default SRS Tables")]
    public void LoadDefaultTables()
    {
        StandardKicks = CreateStandardKicks();
        I_MinoKicks = CreateIMinoKicks();
    }

    private void EnsureDefaultTables()
    {
        if (StandardKicks == null || StandardKicks.Length == 0)
        {
            StandardKicks = CreateStandardKicks();
        }

        if (I_MinoKicks == null || I_MinoKicks.Length == 0)
        {
            I_MinoKicks = CreateIMinoKicks();
        }
    }

    private KickTest[] CreateStandardKicks()
    {
        return new[]
        {
            CreateKickTest(RotationState.North, RotationState.East, 0, 0, -1, 0, -1, 1, 0, -2, -1, -2),
            CreateKickTest(RotationState.East, RotationState.North, 0, 0, 1, 0, 1, -1, 0, 2, 1, 2),
            CreateKickTest(RotationState.East, RotationState.South, 0, 0, 1, 0, 1, -1, 0, 2, 1, 2),
            CreateKickTest(RotationState.South, RotationState.East, 0, 0, -1, 0, -1, 1, 0, -2, -1, -2),
            CreateKickTest(RotationState.South, RotationState.West, 0, 0, 1, 0, 1, 1, 0, -2, 1, -2),
            CreateKickTest(RotationState.West, RotationState.South, 0, 0, -1, 0, -1, -1, 0, 2, -1, 2),
            CreateKickTest(RotationState.West, RotationState.North, 0, 0, -1, 0, -1, -1, 0, 2, -1, 2),
            CreateKickTest(RotationState.North, RotationState.West, 0, 0, 1, 0, 1, 1, 0, -2, 1, -2),
        };
    }

    private KickTest[] CreateIMinoKicks()
    {
        return new[]
        {
            CreateKickTest(RotationState.North, RotationState.East, 0, 0, -2, 0, 1, 0, -2, -1, 1, 2),
            CreateKickTest(RotationState.East, RotationState.North, 0, 0, 2, 0, -1, 0, 2, 1, -1, -2),
            CreateKickTest(RotationState.East, RotationState.South, 0, 0, -1, 0, 2, 0, -1, 2, 2, -1),
            CreateKickTest(RotationState.South, RotationState.East, 0, 0, 1, 0, -2, 0, 1, -2, -2, 1),
            CreateKickTest(RotationState.South, RotationState.West, 0, 0, 2, 0, -1, 0, 2, 1, -1, -2),
            CreateKickTest(RotationState.West, RotationState.South, 0, 0, -2, 0, 1, 0, -2, -1, 1, 2),
            CreateKickTest(RotationState.West, RotationState.North, 0, 0, 1, 0, -2, 0, 1, -2, -2, 1),
            CreateKickTest(RotationState.North, RotationState.West, 0, 0, -1, 0, 2, 0, -1, 2, 2, -1),
        };
    }

    private KickTest CreateKickTest(
        RotationState from,
        RotationState to,
        int x1,
        int y1,
        int x2,
        int y2,
        int x3,
        int y3,
        int x4,
        int y4,
        int x5,
        int y5)
    {
        return new KickTest
        {
            fromRotation = from,
            toRotation = to,
            kickOffsets = new[]
            {
                new Vector2Int(x1, y1),
                new Vector2Int(x2, y2),
                new Vector2Int(x3, y3),
                new Vector2Int(x4, y4),
                new Vector2Int(x5, y5),
            }
        };
    }
}