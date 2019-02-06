using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    public PlayerStatistics savedPlayerData = new PlayerStatistics();
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject); //gameObject
        }
    }

    public int SceneLevel = 0;

    public List<CollectibleItemBasic> collected = new List<CollectibleItemBasic>();
    public List<CollectibleItem> collectibles;
    

    public void CheckCollectibles()
    {
        collectibles = new List<CollectibleItem>();
        collectibles.AddRange(GameObject.FindObjectsOfType<CollectibleItem>());
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        bf.Serialize(file, savedPlayerData);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            savedPlayerData = (PlayerStatistics)bf.Deserialize(file);
            file.Close();
        }
    }
}
