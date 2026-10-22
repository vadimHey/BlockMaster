using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [SerializeField] private Image background;

    private Color _defaultColor;
    private Color _blockColor;

    public void Initialize(int x, int z)
    {
        name = $"Cell_{x}_{z}";
        _defaultColor = background.color;
        _blockColor = _defaultColor;
    }

    public void SetBlockColor(Color color)
    {
        _blockColor = color.a <= 0.01f ? _defaultColor : color;
        background.color = _blockColor;
    }

    public void SetHighlight(Color color)
    {
        background.color = color;
    }

    public void ResetHighlight()
    {
        background.color = _blockColor;
    }
}