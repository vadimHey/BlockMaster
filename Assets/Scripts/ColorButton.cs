using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private Image colorImage;
    [SerializeField] private Outline outline;

    private Color _color;
    private Action<Color> _onClicked;

    public Color Color => _color;

    public void Initialize(Color color, Action<Color> onClicked)
    {
        _color = color;
        _onClicked = onClicked;
        colorImage.color = color;
        SetSelected(false);
    }

    public void HandleClick()
    {
        _onClicked?.Invoke(_color);
    }

    public void SetSelected(bool selected)
    {
        if (outline != null)
            outline.enabled = selected;
    }
}