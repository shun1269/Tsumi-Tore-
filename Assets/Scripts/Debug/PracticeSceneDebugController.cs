using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class PracticeSceneDebugController : MonoBehaviour
{
    [SerializeField] private GameObject _minoPrefab;
    [SerializeField] private Vector2Int _spawnPosition = new Vector2Int(4, 18);
    [SerializeField] private Vector3 _boardOrigin = new Vector3(-2.25f, -4.5f, 0f);
    [SerializeField] private float _cellSize = 0.5f;
    [SerializeField] private float _fallInterval = 0.75f;
    [SerializeField] private int _visibleRows = Field.HEIGHT;

    private readonly List<GameObject> _spawnedBlocks = new List<GameObject>();
    private Field _field;
    private MoveUseCase _moveUseCase;
    private RotateUseCase _rotateUseCase;
    private MinoGenerator _minoGenerator;
    private Tetrimino _currentMino;
    private float _fallTimer;
    private bool _isGameOver;

    [Inject]
    public void Construct(
        Field field,
        MoveUseCase moveUseCase,
        RotateUseCase rotateUseCase,
        MinoGenerator minoGenerator)
    {
        _field = field;
        _moveUseCase = moveUseCase;
        _rotateUseCase = rotateUseCase;
        _minoGenerator = minoGenerator;
    }

    private void Start()
    {
        StartNewRun();
        RefreshVisuals();
    }

    private void Update()
    {
        if (_isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartNewRun();
                RefreshVisuals();
            }

            return;
        }

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
            StartNewRun();
            changed = true;
        }

        _fallTimer += Time.deltaTime;
        if (_fallTimer >= _fallInterval)
        {
            _fallTimer = 0f;
            changed = TryStepDownAndHandleLock() || changed;
        }

        if (changed)
        {
            RefreshVisuals();
        }
    }

    private void StartNewRun()
    {
        _field.Reset();
        _fallTimer = 0f;
        _isGameOver = false;
        SpawnNextMino();
    }

    private void RefreshVisuals()
    {
        ClearBlocks();

        if (_minoPrefab == null)
        {
            return;
        }

        for (int y = 0; y < _visibleRows; y++)
        {
            for (int x = 0; x < Field.WIDTH; x++)
            {
                if (!_field.IsCellOccupied(new Vector2Int(x, y)))
                {
                    continue;
                }

                SpawnBlock(new Vector2Int(x, y));
            }
        }

        if (_currentMino == null)
        {
            return;
        }

        foreach (Vector2Int offset in _currentMino.Data.GetOffsets(_currentMino.Rotation))
        {
            SpawnBlock(_currentMino.Position + offset);
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

    private void SpawnNextMino()
    {
        _currentMino = _minoGenerator.GetNextMino();
        _currentMino.Position = _spawnPosition;
        _currentMino.Rotation = RotationState.North;

        if (!IsValidCurrentPosition())
        {
            _isGameOver = true;
            _currentMino = null;
            Debug.Log("Game Over: spawn position is blocked.", this);
        }
    }

    private bool TryStepDownAndHandleLock()
    {
        if (_moveUseCase.tryMove(_currentMino, Vector2Int.down))
        {
            return true;
        }

        LockCurrentMino();
        return true;
    }

    private void LockCurrentMino()
    {
        _field.PlaceMino(_currentMino);
        int clearedLines = _field.ClearFullLines();
        if (clearedLines > 0)
        {
            Debug.Log($"Cleared {clearedLines} line(s).", this);
        }

        SpawnNextMino();
    }

    private bool IsValidCurrentPosition()
    {
        if (_currentMino == null)
        {
            return false;
        }

        foreach (Vector2Int offset in _currentMino.Data.GetOffsets(_currentMino.Rotation))
        {
            Vector2Int blockPos = _currentMino.Position + offset;
            if (_field.IsCellOccupied(blockPos))
            {
                return false;
            }
        }

        return true;
    }

    private void SpawnBlock(Vector2Int gridPosition)
    {
        if (gridPosition.y < 0 || gridPosition.y >= _visibleRows)
        {
            return;
        }

        Vector3 worldPosition = _boardOrigin + new Vector3(gridPosition.x * _cellSize, gridPosition.y * _cellSize, 0f);
        GameObject block = Instantiate(_minoPrefab, worldPosition, Quaternion.identity, transform);
        block.transform.localScale = Vector3.one * _cellSize;
        _spawnedBlocks.Add(block);
    }
}
