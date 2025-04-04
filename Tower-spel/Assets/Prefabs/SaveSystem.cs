using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private string path;

    private void Awake()
    {
        path = Application.persistentDataPath + "/save.json";
    }

    [System.Serializable]
    public class SaveData
    {
        public int level;
        public float health;
        public float[] position;

        public SaveData(int lvl, float hp, Vector3 pos)
        {
            level = lvl;
            health = hp;
            position = new float[] { pos.x, pos.y, pos.z };
        }
    }

    public void SaveGame(int level, float health, Vector3 position)
    {
        SaveData data = new SaveData(level, health, position);
        File.WriteAllText(path, JsonUtility.ToJson(data));
        Debug.Log("Game Saved!");
    }

    public SaveData LoadGame()
    {
        if (File.Exists(path))
        {
            return JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
        }

        Debug.LogWarning("No save file found!");
        return null;
    }
}
