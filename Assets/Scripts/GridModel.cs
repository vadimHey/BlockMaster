using UnityEngine;
using System.Collections.Generic;

public class GridModel : MonoBehaviour
{
    private const int WidthConst = 16;
    private const int HeightConst = 16;

    private List<BlockData>[,] _cells;
    private Color[,] _colors;

    public int Width => WidthConst;
    public int Height => HeightConst;

    private void Awake()
    {
        _cells = new List<BlockData>[Width, Height];
        for (int x = 0; x < Width; x++)
            for (int z = 0; z < Height; z++)
                _cells[x, z] = new List<BlockData>();
    }

    public float GetTopHeight(int x, int z)
    {
        if (!IsInside(x, z)) return 0f;
        List<BlockData> list = _cells[x, z];
        return list.Count == 0 ? 0f : list[^1].Top;
    }

    public Color GetTopColor(int x, int z)
    {
        if (!IsInside(x, z)) return Color.clear;
        List<BlockData> list = _cells[x, z];
        return list.Count == 0 ? Color.clear : list[^1].Color;
    }

    public IReadOnlyList<BlockData> GetColumn(int x, int z)
    {
        if (!IsInside(x, z)) return null;
        return _cells[x, z];
    }

    public void AddBlock(int x, int z, float bottom, float top, Color color)
    {
        if (!IsInside(x, z)) return;
        _cells[x, z].Add(new(bottom, top, color));
    }

    public void RemoveTopBlock(int x, int z)
    {
        if (!IsInside(x, z)) return;
        if (_cells[x, z].Count > 0)
            _cells[x, z].RemoveAt(_cells[x, z].Count - 1);
    }

    public void Clear()
    {
        for (int x = 0; x < Width; x++)
            for (int z = 0; z < Height; z++)
                _cells[x, z].Clear();
    }

    private bool IsInside(int x, int z)
    {
        return x >= 0 && x < Width && z >= 0 && z < Height;
    }

    public GridSaveData ToSaveData()
    {
        GridSaveData data = new(Width, Height);

        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Height; z++)
            {
                List<BlockData> column = _cells[x, z];
                if (column.Count == 0) continue;

                CellSaveData cell = new(x, z);

                foreach (BlockData block in column)
                {
                    cell.blocks.Add(new(block.Bottom, block.Top, block.Color.r, block.Color.g, block.Color.b, block.Color.a));
                }

                data.cells.Add(cell);
            }
        }

        return data;
    }

    public void LoadFromSaveData(GridSaveData data)
    {
        Clear();

        foreach (CellSaveData cell in data.cells)
        {
            foreach (BlockSaveData block in cell.blocks)
            {
                Color color = new(block.R, block.G, block.B, block.A);
                AddBlock(cell.X, cell.Z, block.Bottom, block.Top, color);
            }
        }
    }
}