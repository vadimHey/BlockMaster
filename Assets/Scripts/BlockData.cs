using UnityEngine;

public class BlockData
{
    public float Bottom { get;  }   
    public float Top { get; }
    public Color Color { get; }
    public float Height => Top - Bottom;

    public BlockData(float bottom, float top, Color color)
    {
        Bottom = bottom;
        Top = top;
        Color = color;
    }
}