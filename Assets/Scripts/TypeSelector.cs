using System;
using UnityEngine;

public class TypeSelector : MonoBehaviour
{
    public event Action<bool> OnTypeChanged;

    public void SelectBig()
    {
        OnTypeChanged?.Invoke(true);
    }

    public void SelectSmall()
    {
        OnTypeChanged?.Invoke(false);
    }
}
