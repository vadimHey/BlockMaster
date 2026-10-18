using UnityEngine;

public class GridModel : MonoBehaviour
{
    private const int WidthConst = 16;
    private const int HeightConst = 16;
    private float[,] _cells;

    public int Width => WidthConst;
    public int Height => HeightConst;

    private void Awake()
    {
        _cells = new float[WidthConst, HeightConst];
    }

    public float GetHeight(int x, int z)
    {
        if (!IsInside(x, z)) return 0;
        return _cells[x, z];
    }

    public void SetHeight(int x, int z, float value)
    {
        if (!IsInside(x, z)) return;
        _cells[x, z] = value;
    }

    public void AddHeight(int x, int z, float delta)
    {
        if (!IsInside(x, z)) return;
        _cells[x, z] += delta;
    }

    public void Clear()
    {
        _cells = new float[WidthConst, HeightConst];
    }

    private bool IsInside(int x, int z)
    {
        return x >= 0 && x < WidthConst && z >= 0 && z < HeightConst;
    }
}
