using System;
using UnityEngine;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private Image colorImage;

    private Color _color;
    private Action<Color> _onClicked;

    public void Initialize(Color color, Action<Color> onClicked)
    {
        _color = color;
        _onClicked = onClicked;
        colorImage.color = color;
    }

    public void HandleClick()
    {
        _onClicked?.Invoke(_color);
    }
}