using System;
using UnityEngine;

public class ColorPaletteView : MonoBehaviour
{
    [SerializeField] private Transform palettePanel;
    [SerializeField] private GameObject colorButtonPrefab;

    private readonly Color[] _colors =
    {
        new(0.9f, 0.2f, 0.2f), // красный
        new(0.2f, 0.6f, 0.9f), // синий
        new(0.3f, 0.8f, 0.3f), // зелёный
        new(0.95f, 0.85f, 0.2f), // жёлтый
        new(0.9f, 0.5f, 0.1f), // оранжевый
        new(0.6f, 0.3f, 0.8f), // фиолетовый
        new(0.1f, 0.1f, 0.1f), // чёрный
        new(0.95f, 0.95f, 0.95f) // белый
    };

    public event Action<Color> OnColorSelected;

    private void Start()
    {
        foreach (Color color in _colors)
        {
            GameObject buttonObj = Instantiate(colorButtonPrefab, palettePanel);
            ColorButton button = buttonObj.GetComponent<ColorButton>();
            button.Initialize(color, HandleColorClicked);
        }
    }

    private void HandleColorClicked(Color color)
    {
        OnColorSelected?.Invoke(color);
    }
}
