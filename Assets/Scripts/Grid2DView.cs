using System;
using UnityEngine;
using UnityEngine.UI;

public class Grid2DView : MonoBehaviour
{
    [SerializeField] private RectTransform gridPanel;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Color highlightColor = new Color(1f, 1f, 0.5f, 0.8f);

    public event Action<int, int, int, int> OnAreaSelected;

    private const int Width = 16, Height = 16;

    private CellView[,] _cellViews;
    private bool _isDragging;
    private int _startX, _startZ;
    private int _currentX, _currentZ;

    private void Start()
    {
        BuildGrid();
    }

    private void BuildGrid()
    {
        if (gridPanel == null || cellPrefab == null) return;

        _cellViews = new CellView[Width, Height];

        for (int z = 0; z < Height; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                GameObject cellObj = Instantiate(cellPrefab, gridPanel);
                CellView cell = cellObj.GetComponent<CellView>();

                cell.Initialize(x, z);
                _cellViews[x, z] = cell;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && TryGetCellUnderMouse(out int x, out int z))
        {
            _isDragging = true;
            _startX = x;
            _startZ = z;
            _currentX = x;
            _currentZ = z;
            HighlightArea();
        }

        if (_isDragging && Input.GetMouseButton(0) && TryGetCellUnderMouse(out x, out z))
        {
            if (x != _currentX || z != _currentZ)
            {
                _currentX = x;
                _currentZ = z;
                HighlightArea();
            }
        }

        if (_isDragging && Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            ClearHighlight();
            OnAreaSelected?.Invoke(_startX, _startZ, _currentX, _currentZ);
        }
    }

    private bool TryGetCellUnderMouse(out int cellX, out int cellZ)
    {
        cellX = -1;
        cellZ = -1;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                gridPanel, Input.mousePosition, null, out Vector2 localPoint))
            return false;

        Rect rect = gridPanel.rect;
        float localX = localPoint.x - rect.xMin;
        float localY = rect.yMax - localPoint.y;

        if (localX < 0 || localY < 0 || localX >= rect.width || localY >= rect.height)
            return false;

        int col = Mathf.FloorToInt(localX / GetCellStrideX());
        int row = Mathf.FloorToInt(localY / GetCellStrideY());

        if (col < 0 || col >= Width || row < 0 || row >= Height)
            return false;

        cellX = col;
        cellZ = row;
        return true;
    }

    private float GetCellStrideX()
    {
        GridLayoutGroup layout = gridPanel.GetComponent<GridLayoutGroup>();
        return layout.cellSize.x + layout.spacing.x;
    }

    private float GetCellStrideY()
    {
        GridLayoutGroup layout = gridPanel.GetComponent<GridLayoutGroup>();
        return layout.cellSize.y + layout.spacing.y;
    }

    private void HighlightArea()
    {
        ClearHighlight();

        int minX = Mathf.Min(_startX, _currentX);
        int maxX = Mathf.Max(_startX, _currentX);
        int minZ = Mathf.Min(_startZ, _currentZ);
        int maxZ = Mathf.Max(_startZ, _currentZ);

        for (int x = minX; x <= maxX; x++)
        {
            for (int z = minZ; z <= maxZ; z++)
            {
                _cellViews[x, z].SetColor(highlightColor);
            }
        }
    }

    private void ClearHighlight()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Height; z++)
            {
                _cellViews[x, z].ResetColor();
            }
        }
    }

    private void SetCellColor(int x, int z, Color color)
    {
        if (x < 0 || x >= Width || z < 0 || z >= Height) return;
        _cellViews[x, z].SetColor(color);
    }
}
