using System.ComponentModel.DataAnnotations.Schema;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "DPC/Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("操作設定")]
    public float DAS_Delay = 0.3f; // Auto-Repeatまでの遅延
    public float ARR_Speed = 0.05f; // Auto-Repeatの速度

    [Header("ロックダウン設定")]
    public float LockDown_Time = 0.5f; // 設置タイマー
    public int LockDown_MoveLimit = 15; // 移動・回転の回数制限
}