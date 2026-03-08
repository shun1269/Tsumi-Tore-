using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

public class PracticeSceneDebugController : MonoBehaviour
{
    [SerializeField] private GameObject _minoPrefab;
    [SerializeField] private MinoType _spawnType = MinoType.T;
    [SerializeField] private Vector2Int _spawnPosition = new Vector2Int(4, 10);
    [SerializeField] private Vector3 _boardOrigin = new Vector3(-2.25f, -4.5f, 0f);
    [SerializeField] private float _cellSize = 0.5f;

    private readonly List<GameObject> _spawnedBlocks = new List<GameObject>();
    private MoveUseCase _moveUseCase;
    private RotateUseCase _rotateUseCase;
    private TetriminoData[] _tetriminoDatas;
    private Tetrimino _currentMino;

    [Inject]
    public void Construct(
        MoveUseCase moveUseCase,
        RotateUseCase rotateUseCase,
        TetriminoData[] tetriminoDatas)
    {
        _moveUseCase = moveUseCase;
        _rotateUseCase = rotateUseCase;
        _tetriminoDatas = tetriminoDatas;
    }

    private void Start()
    {
        SpawnSelectedMino();
        RefreshVisuals();
    }

    private void Update()
    {
        if (_currentMino == null)
        {
            return;
        }

        bool changed = false;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            changed = _moveUseCase.tryMove(_currentMino, Vector2Int.left) || changed;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            changed = _moveUseCase.tryMove(_currentMino, Vector2Int.right) || changed;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            changed = _moveUseCase.tryMove(_currentMino, Vector2Int.down) || changed;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            changed = _rotateUseCase.TryRotate(_currentMino, true) || changed;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            changed = _rotateUseCase.TryRotate(_currentMino, false) || changed;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SpawnSelectedMino();
            changed = true;
        }

        if (changed)
        {
            RefreshVisuals();
        }
    }

    private void SpawnSelectedMino()
    {
        if (_tetriminoDatas == null || _tetriminoDatas.Length == 0)
        {
            Debug.LogWarning("TetriminoData is not assigned.", this);
            _currentMino = null;
            return;
        }

        TetriminoData data = _tetriminoDatas.FirstOrDefault(tetriminoData => tetriminoData.Type == _spawnType);
        if (data == null)
        {
            Debug.LogWarning($"TetriminoData for {_spawnType} was not found.", this);
            _currentMino = null;
            return;
        }

        _currentMino = new Tetrimino(data)
        {
            Position = _spawnPosition
        };
    }

    private void RefreshVisuals()
    {
        ClearBlocks();

        if (_currentMino == null || _minoPrefab == null)
        {
            return;
        }

        foreach (Vector2Int offset in _currentMino.Data.GetOffsets(_currentMino.Rotation))
        {
            Vector2Int gridPosition = _currentMino.Position + offset;
            Vector3 worldPosition = _boardOrigin + new Vector3(gridPosition.x * _cellSize, gridPosition.y * _cellSize, 0f);
            GameObject block = Instantiate(_minoPrefab, worldPosition, Quaternion.identity, transform);
            block.transform.localScale = Vector3.one * _cellSize;
            _spawnedBlocks.Add(block);
        }
    }

    private void ClearBlocks()
    {
        foreach (GameObject block in _spawnedBlocks)
        {
            if (block != null)
            {
                Destroy(block);
            }
        }

        _spawnedBlocks.Clear();
    }
}
