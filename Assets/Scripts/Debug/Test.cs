using System.Collections.Generic;
using Unity.Mathematics;

// using Unity.Mathematics; // このコードでは不要
using UnityEngine;

public class Test : MonoBehaviour
{
    public TetriminoData tetriminoData;
    public RotationState rotationState;
    public GameObject minoPrefab;

    private List<GameObject> spawnedMinos = new List<GameObject>();

    private void OnValidate()
    {
        // データをチェック
        if (minoPrefab == null || tetriminoData == null)
        {
            ClearMinos(); 
            return;
        }

        ClearMinos();

        Vector2Int[] offsets = tetriminoData.GetOffsets(rotationState);
        foreach (var offset in offsets)
        {
            Vector3 position = new Vector3(offset.x, offset.y, 0);
            
            // OnValidate内では Instantiate を使います
            GameObject mino = Instantiate(minoPrefab, position,quaternion.identity, this.transform);
            mino.transform.localPosition = position; // 親を設定後にローカル座標を指定
            
            spawnedMinos.Add(mino);
        }
    }
    
    private void ClearMinos()
    {
        // 既存のミノを削除
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        // メモリ上のリストもクリアする
        spawnedMinos.Clear();
    }
}