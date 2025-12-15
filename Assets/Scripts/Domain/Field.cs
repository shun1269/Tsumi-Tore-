using System.Xml.Serialization;
using UnityEngine;

public class Field
{
    // [x,y]でアクセス
    // 高さを40にしておくとバッファゾーンも管理しやすい
    private readonly MinoType[,] _grid = new MinoType[WIDTH, HEIGHT_BUFFER];
    public const int WIDTH = 10;
    public const int HEIGHT = 20; // 見えてる高さ
    public const int HEIGHT_BUFFER = 40; // バッファゾーンの高さ

    // フィールドの初期化
    public Field()
    {
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT_BUFFER; y++)
            {
                _grid[x, y] = MinoType.None;
            }
        }
    }

    // 指定した位置がフィールド内で占有されているかをチェック MinoType.Noneであればtrueを返す
    public bool IsCellOccupied(Vector2Int pos)
    {
        if (pos.x < 0 || pos.x >= WIDTH || pos.y < 0)
        {
            return true; // フィールド外は占有されているとみなす
        }

        if (pos.y >= HEIGHT_BUFFER)
        {
            return false; // バッファゾーン外（上）は占有されていないとみなす
        }

        return _grid[pos.x, pos.y] != MinoType.None; // None以外は占有されている
    }

    // ミノをグリッドに固定する
    public void PlaceMino(Tetrimino mino)
    {
        foreach(var offset in mino.Data.GetOffsets(mino.Rotation))
        {
            Vector2Int gridPos = mino.Position + offset;
            if (gridPos.x >= 0 && gridPos.x < WIDTH && gridPos.y >= 0 && gridPos.y < HEIGHT_BUFFER)
            {
                _grid[gridPos.x, gridPos.y] = mino.Type;
            }
        }
    }

    public int ClearFullLines()
    {
        int linesCleared = 0;

        for (int y = 0; y < HEIGHT_BUFFER; y++)
        {
            bool isFullLine = true;
            for (int x = 0; x < WIDTH; x++)
            {
                if (_grid[x, y] == MinoType.None)
                {
                    isFullLine = false;
                    break;
                }
            }

            if (isFullLine)
            {
                linesCleared++;
                // ラインをクリアして上のラインを下に落とす
                for (int row = y; row < HEIGHT_BUFFER - 1; row++)
                {
                    for (int col = 0; col < WIDTH; col++)
                    {
                        _grid[col, row] = _grid[col, row + 1];
                    }
                }
                // 最上行を空にする
                for (int col = 0; col < WIDTH; col++)
                {
                    _grid[col, HEIGHT_BUFFER - 1] = MinoType.None;
                }
                y--; // チェックする行を再度確認
            }
        }

        return linesCleared;
    }
}