using VContainer;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class MinoGenerator
{
    private readonly TetriminoData[] _allTetriminoData;
    private readonly Queue<Tetrimino> _nextQueue = new Queue<Tetrimino>();
    private readonly List<MinoType> _bag = new List<MinoType>();

    [Inject]
    public MinoGenerator(TetriminoData[] allTetriminoData)
    {
        _allTetriminoData = allTetriminoData;

        // バッグの初期化
        foreach(var data in _allTetriminoData)
        {
            _bag.Add(data.Type);
        }

        RefillNextQueue();
    }
    public Tetrimino GetNextMino()
    {
        if (_nextQueue.Count <= 7)
        {
            RefillNextQueue();
        }

        return _nextQueue.Dequeue();
    }

    private void RefillNextQueue()
    {
        // バッグをシャッフルして
        for(int i=_bag.Count -1; i>0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = _bag[i];
            _bag[i] = _bag[j];
            _bag[j] = temp;
        }

        // キューに追加
        foreach (var minoType in _bag)
        {
            var data = _allTetriminoData.First(t => t.Type == minoType);
            var tetrimino = new Tetrimino(data);
            _nextQueue.Enqueue(tetrimino);
        }
    }
}