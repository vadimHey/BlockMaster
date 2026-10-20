using System;
using UnityEngine;
using UnityEngine.UI;

public class TypeSelector : MonoBehaviour
{
    [SerializeField] private Outline bigOutline;
    [SerializeField] private Outline smallOutline;

    public event Action<bool> OnTypeChanged;

    public void Start()
    {
        UpdateVisual(true);
    }

    public void SelectBig()
    {
        UpdateVisual(true);
        OnTypeChanged?.Invoke(true);
    }

    public void SelectSmall()
    {
        UpdateVisual(false);
        OnTypeChanged?.Invoke(false);
    }

    private void UpdateVisual(bool isBig)
    {
        bigOutline.enabled = isBig;
        smallOutline.enabled = !isBig;
    }
}
