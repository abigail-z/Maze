using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public string fileName;
    public Transform player;
    public Transform enemy;
    public HUDBehaviour hud;

	void Awake ()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
	}

    void Update ()
    {
        if (Input.GetButtonDown("Save"))
            Save();

        if (Input.GetButtonDown("Load"))
            Load();
    }

    public void Save ()
    {
        SaveData sd = new SaveData
        {
            score = (int)GameManager.Instance.Score,
            playerPos = player.position,
            enemyPos = enemy.position
        };

        string json = JsonUtility.ToJson(sd);
        PlayerPrefs.SetString(fileName, json);

        hud.UpdateSaveStatus("Saved");
    }

    public void Load ()
    {
        string json = PlayerPrefs.GetString(fileName);

        if (json.Length > 0)
        {
            SaveData sd = JsonUtility.FromJson<SaveData>(json);

            GameManager.Instance.Score = (uint)sd.score;
            player.position = sd.playerPos;
            enemy.position = sd.enemyPos;

            hud.UpdateSaveStatus("Loaded");
            hud.UpdateHUD();
        }
        else
        {
            hud.UpdateSaveStatus("No file to load");
        }
    }

    [Serializable]
    struct SaveData
    {
        public int score;
        public SerializableVector3 playerPos;
        public SerializableVector3 enemyPos;
    }

    [Serializable]
    struct SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        public static implicit operator Vector3(SerializableVector3 rValue)
        {
            return new Vector3(rValue.x, rValue.y, rValue.z);
        }

        public static implicit operator SerializableVector3(Vector3 rValue)
        {
            return new SerializableVector3
            {
                x = rValue.x,
                y = rValue.y,
                z = rValue.z,
            };
        }
    }
}
