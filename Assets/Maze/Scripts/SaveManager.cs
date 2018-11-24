using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get { return instance; } }
    private static SaveManager instance;

    public string fileName;
    public Transform player;
    public Transform enemy;
    public HUDBehaviour hud;

	void Awake ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        fileName = Application.persistentDataPath + "/" + fileName + ".dat";
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

        // save and close
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(fileName);
        bf.Serialize(file, sd);
        file.Close();

        hud.UpdateSaveStatus("Saved");
    }

    public void Load ()
    {
        if (File.Exists(fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(fileName, FileMode.Open);
            SaveData sd = (SaveData)bf.Deserialize(file);
            file.Close();

            GameManager.Instance.Score = (uint)sd.score;
            player.position = sd.playerPos;
            enemy.position = sd.enemyPos;

            hud.UpdateSaveStatus("Loaded");
        }
        else
        {
            hud.UpdateSaveStatus("No save file to load");
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
