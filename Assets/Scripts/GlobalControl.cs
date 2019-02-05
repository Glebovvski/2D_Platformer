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

    public List<CollectibleItem> collected = new List<CollectibleItem>();
    public List<CollectibleItem> collectibles;

    //private void OnLevelWasLoaded(int level)
    //{
    //    collectibles = new List<CollectibleItem>();
    //    collectibles.AddRange(GameObject.FindObjectsOfType<CollectibleItem>());
    //    Debug.Log("collectibles: " + collectibles.Count);
    //    Debug.Log("collected: " + collected.Count);
    //    
    //}

    private void OnPreRender()
    {
        //if(collectibles!=null && collectibles.Count > 0)
        //{
        //    foreach (var item in collectibles)
        //    {
        //        if (collected.Contains(item))
        //            Destroy(item.gameObject);
        //    }
        //}
    }

    public void CheckCollectibles()
    {
        collectibles = new List<CollectibleItem>();
        collectibles.AddRange(GameObject.FindObjectsOfType<CollectibleItem>());
        Debug.Log("collectibles: " + collectibles.Count);
        Debug.Log("collected: " + collected.Count);
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
