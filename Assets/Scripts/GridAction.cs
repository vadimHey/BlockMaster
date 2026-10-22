using UnityEngine;
using System.Collections.Generic;

public class CellChange
{
    public int X { get; }
    public int Z { get; }
    public float OldHeight { get; }
    public float NewHeight { get; }
    public Color OldColor { get; }
    public Color NewColor { get; }
    public BlockData AddedBlock { get; }

    public CellChange(int x, int z, float oldHeight, float newHeight, Color oldColor, Color newColor, BlockData addedBlock)
    {
        X = x; Z = z;
        OldHeight = oldHeight; NewHeight = newHeight;
        OldColor = oldColor; NewColor = newColor;
        AddedBlock = addedBlock;
    }
}

public class GridAction
{
    public List<CellChange> Changes = new();
}