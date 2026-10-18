using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    [SerializeField] private GameObject bigBlockPrefab;
    [SerializeField] private GameObject smallBlockPrefab;
    [SerializeField] private GridModel gridModel;
    [SerializeField] private Grid2DView grid2DView;
    [SerializeField] private ColorPaletteView colorPaletteView;
    [SerializeField] private TypeSelector typeSelector;

    private const float CellSize = 1f, HalfCellSize = CellSize / 2f;
    private const float BaseHeight = 0.25f;
    private const float BigBlockHeight = 1.06f, SmallBlockHeight = 1.1f / 3f;

    private GameObject _currentBlockPrefab;
    private float _currentBlockHeight;
    private Color _currentColor = Color.white;

    private void Start()
    {
        SelectBigBlock();
        grid2DView.OnAreaSelected += PlaceBlocksInArea;

        if (colorPaletteView != null)
            colorPaletteView.OnColorSelected += SetColor;

        if (typeSelector != null)
            typeSelector.OnTypeChanged += HandleTypeChanged;
    }

    private void OnDestroy()
    {
        grid2DView.OnAreaSelected -= PlaceBlocksInArea;

        if (colorPaletteView != null)
            colorPaletteView.OnColorSelected -= SetColor;

        if (typeSelector != null)
            typeSelector.OnTypeChanged -= HandleTypeChanged;
    }

    private void SetColor(Color color)
    {
        _currentColor = color;
    }

    private void HandleTypeChanged(bool isBig)
    {
        if (isBig) SelectBigBlock();
        else SelectSmallBlock();
    }

    private void PlaceBlocksInArea(int startX, int startZ, int endX, int endZ)
    {
        int minX = Mathf.Min(startX, endX);
        int maxX = Mathf.Max(startX, endX);
        int minZ = Mathf.Min(startZ, endZ);
        int maxZ = Mathf.Max(startZ, endZ);

        float maxHeight = 0f;

        for (int x = minX; x <= maxX; x++)
        {
            for (int z = minZ; z <= maxZ; z++)
            {
                if (x < 0 || x >= gridModel.Width) continue;
                if (z < 0 || z >= gridModel.Height) continue;

                float h = gridModel.GetHeight(x, z);
                if (h > maxHeight) maxHeight = h;
            }
        }

        for (int x = minX; x <= maxX; x++)
        {
            for (int z = minZ; z <= maxZ; z++)
            {
                if (x < 0 || x >= gridModel.Width) continue;
                if (z < 0 || z >= gridModel.Height) continue;

                float worldZ = (gridModel.Height - 1 - z);

                Vector3 spawnPos = new(x * CellSize + HalfCellSize, BaseHeight + maxHeight, worldZ * CellSize + HalfCellSize);

                GameObject block = Instantiate(_currentBlockPrefab, spawnPos, Quaternion.identity);
                ApplyColor(block, _currentColor);

                gridModel.SetHeight(x, z, maxHeight + _currentBlockHeight);
            }
        }
    }

    private void ApplyColor(GameObject block, Color color)
    {
        Renderer[] renderers = block.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = color;
        }
    }

    private void SelectBigBlock()
    {
        _currentBlockPrefab = bigBlockPrefab;
        _currentBlockHeight = BigBlockHeight;
    }

    private void SelectSmallBlock()
    {
        _currentBlockPrefab = smallBlockPrefab;
        _currentBlockHeight = SmallBlockHeight;
    }
}