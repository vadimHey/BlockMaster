using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CellView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image background;

    private Color _defaultColor;
    public int X { get; private set; }
    public int Z { get; private set; }

    public event Action<int, int> OnClicked;

    public void Initialize(int x, int z)
    {
        X = x;
        Z = z;
        name = $"Cell_{x}_{z}";
        _defaultColor = background.color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked?.Invoke(X, Z);
    }

    public void SetColor(Color color)
    {
        background.color = color;
    }

    public void ResetColor()
    {
        background.color = _defaultColor;
    }
}
