using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            Destroy(gameObject);
        }
    }

    public List<CollectibleItem> collected = new List<CollectibleItem>();
    List<CollectibleItem> collectibles;

    private void OnLevelWasLoaded(int level)
    {
        collectibles = new List<CollectibleItem>();
        collectibles.AddRange(GameObject.FindObjectsOfType<CollectibleItem>());
        Debug.Log("collectibles: " + collectibles.Count);
        Debug.Log("collected: " + collected.Count);
        
    }

    private void OnPreRender()
    {
        if(collectibles!=null && collectibles.Count > 0)
        {
            foreach (var item in collectibles)
            {
                if (collected.Contains(item))
                    Destroy(item.gameObject);
            }
        }
    }

    public void CheckCollectibles()
    {
        
    }
}
