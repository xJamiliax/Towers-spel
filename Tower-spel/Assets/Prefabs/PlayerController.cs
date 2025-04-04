using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public int level = 1;
    public float health = 100f;
    private SaveSystem saveSystem;
    // Start is called before the first frame update
    void Start()
    {
        saveSystem = FindObjectOfType<SaveSystem>();
        SaveSystem.SaveData data = saveSystem.LoadGame();

        if (data != null)
        {
            level = data.level;
            health = data.health;
            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            saveSystem.SaveGame(level, health, transform.position);

        if (Input.GetKeyDown(KeyCode.L))
        {
            SaveSystem.SaveData data = saveSystem.LoadGame();
            if (data != null)
                transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }
    }
}
