using UnityEngine;
using System.Collections.Generic;

public class Scene3DView : MonoBehaviour
{
    [SerializeField] private GridModel gridModel;
    [SerializeField] private GameObject bigBlockPrefab;
    [SerializeField] private GameObject smallBlockPrefab;
    [SerializeField] private Transform blocksParent;

    private const float CellSize = 1f, HalfCellSize = CellSize / 2f;
    private const float BaseHeight = 0.25f, BigBlockHeight = 1.44f;

    private readonly Dictionary<Vector2Int, List<GameObject>> _columns = new();

    public void RebuildColumn(int x, int z)
    {
        Vector2Int key = new(x, z);

        if (_columns.TryGetValue(key, out List<GameObject> oldBlocks))
        {
            foreach (GameObject block in oldBlocks)
            {
                Destroy(block);
            }
            oldBlocks.Clear();
        }
        else _columns[key] = new List<GameObject>();

        IReadOnlyList<BlockData> column = gridModel.GetColumn(x, z);
        if (column == null || column.Count == 0) return;

        float worldZ = gridModel.Height - z - 1;

        foreach (BlockData data in column)
        {
            bool isBig = data.Height >= BigBlockHeight - 0.001f;
            GameObject prefab = isBig ? bigBlockPrefab : smallBlockPrefab;
            Vector3 pos = new(x * CellSize + HalfCellSize, BaseHeight + data.Bottom, worldZ * CellSize + HalfCellSize);
            GameObject block = Instantiate(prefab, pos, Quaternion.identity, blocksParent);
            ApplyColor(block, data.Color);
            _columns[key].Add(block);
        }
    }

    public void ClearAll()
    {
        foreach (List<GameObject> column in _columns.Values)
        {
            foreach (GameObject block in column)
            {
                Destroy(block);
            }
        }
        _columns.Clear();
    }

    private void ApplyColor(GameObject block, Color color)
    {
        Renderer[] renderers = block.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = color;
        }
    }
}