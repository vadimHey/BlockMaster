using System;
using System.Collections.Generic;

[Serializable]
public class BlockSaveData
{
    public float Bottom { get; }
    public float Top { get; }
    public float R { get; }
    public float G { get; }
    public float B { get; }
    public float A { get; }

    public BlockSaveData(float bottom, float top, float r, float g, float b, float a)
    {
        Bottom = bottom; Top = top;
        R = r; G = g; B = b; A = a;
    }
}

[Serializable]
public class CellSaveData
{
    public int X { get; }
    public int Z { get; }
    public List<BlockSaveData> blocks = new();

    public CellSaveData(int x, int z)
    {
        X = x; 
        Z = z;
    }
}

[Serializable]
public class GridSaveData
{
    public int Width { get; }
    public int Height { get; }
    public List<CellSaveData> cells = new();

    public GridSaveData(int width, int height)
    {
        Width = width; 
        Height = height;
    }
}