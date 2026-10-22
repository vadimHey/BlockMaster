using UnityEngine;

public class FileMenuController : MonoBehaviour
{
    [SerializeField] private GameObject fileMenu;

    private void Start()
    {
        fileMenu.SetActive(false);
    }

    public void ToggleMenu()
    {
        fileMenu.SetActive(!fileMenu.activeSelf);
    }

    public void CloseMenu()
    {
        fileMenu.SetActive(false);
    }

    public void CloseApplication()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}