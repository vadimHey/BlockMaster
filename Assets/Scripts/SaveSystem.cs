using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static void Save(GridSaveData data, string fileName)
    {
        string json = JsonUtility.ToJson(data, true);
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");
        File.WriteAllText(path, json);
        Debug.Log($"Сохранено: {path}");
    }

    public static GridSaveData Load(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");

        if (!File.Exists(path))
        {
            Debug.LogWarning($"Файл не найден: {path}");
            return null;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<GridSaveData>(json);
    }

    public static bool Exists(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName + ".json");
        return File.Exists(path);
    }
}