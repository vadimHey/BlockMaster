using System;
using UnityEngine;
using System.Collections.Generic;

public class ColorPaletteView : MonoBehaviour
{
    [SerializeField] private Transform palettePanel;
    [SerializeField] private GameObject colorButtonPrefab;

    private const int DefaultColorIndex = 0;
    private readonly Color[] _colors =
    {
        new(0.95f, 0.95f, 0.95f), // белый
        new(0.95f, 0.6f, 0.75f), // розовый
        new(0.9f, 0.2f, 0.2f),   // красный
        new(0.9f, 0.5f, 0.1f),   // оранжевый
        new(0.95f, 0.85f, 0.2f), // жёлтый
        new(0.3f, 0.8f, 0.3f),   // зелёный
        new(0.1f, 0.5f, 0.5f),   // бирюзовый
        new(0.2f, 0.6f, 0.9f),   // синий
        new(0.6f, 0.3f, 0.8f),   // фиолетовый
        new(0.5f, 0.35f, 0.2f),  // коричневый
        new(0.5f, 0.5f, 0.5f),   // серый
        new(0.1f, 0.1f, 0.1f)    // чёрный
    };
    private readonly List<ColorButton> _buttons = new();

    public event Action<Color> OnColorSelected;

    private void Start()
    {
        foreach (Color color in _colors)
        {
            GameObject buttonObj = Instantiate(colorButtonPrefab, palettePanel);
            ColorButton button = buttonObj.GetComponent<ColorButton>();
            button.Initialize(color, HandleColorClicked);
            _buttons.Add(button);
        }

        _buttons[DefaultColorIndex].SetSelected(true);
        OnColorSelected?.Invoke(_colors[DefaultColorIndex]);
    }

    private void HandleColorClicked(Color color)
    {
        foreach (ColorButton button in _buttons)
        {
            button.SetSelected(button.Color == color);
        }

        OnColorSelected?.Invoke(color);
    }
}