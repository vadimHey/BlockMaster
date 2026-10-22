using UnityEngine;

public class BlockPlacer : MonoBehaviour
{
    [SerializeField] private GameObject bigBlockPrefab;
    [SerializeField] private GameObject smallBlockPrefab;
    [SerializeField] private GridModel gridModel;
    [SerializeField] private Grid2DView grid2DView;
    [SerializeField] private Scene3DView scene3DView;
    [SerializeField] private ColorPaletteView colorPaletteView;
    [SerializeField] private TypeSelector typeSelector;
    [SerializeField] private ActionHistory actionHistory;

    private const float BigBlockHeight = 1.44f, SmallBlockHeight = BigBlockHeight / 3f;

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

                float height = gridModel.GetTopHeight(x, z);
                if (height > maxHeight) maxHeight = height;
            }
        }

        GridAction action = new();

        for (int x = minX; x <= maxX; x++)
        {
            for (int z = minZ; z <= maxZ; z++)
            {
                if (x < 0 || x >= gridModel.Width) continue;
                if (z < 0 || z >= gridModel.Height) continue;

                float bottom = maxHeight;
                float top = maxHeight + _currentBlockHeight;

                BlockData newBlock = new(bottom, top, _currentColor);

                Color oldColor = gridModel.GetTopColor(x, z);
                float oldTop = gridModel.GetTopHeight(x, z);

                gridModel.AddBlock(x, z, bottom, top, _currentColor);
                grid2DView.SetCellColor(x, z, _currentColor);
                scene3DView.RebuildColumn(x, z);

                action.Changes.Add(new(x, z, oldTop, top, oldColor, _currentColor, newBlock));
            }
        }

        actionHistory.Push(action);
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