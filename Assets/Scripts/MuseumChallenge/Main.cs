using UnityEngine;
using System.Collections.Generic;

public class Main : MonoBehaviour
{
    [Header("Items")]
    public GameObject itemRoot;
    private List<Item> items = new List<Item>();

    [Header("Flags")]
    public GameObject flagRoot;
    private List<Flag> flags = new List<Flag>();

    [Header("Platforms")]
    public GameObject platformRoot;
    private List<Platform> platforms = new List<Platform>();

    void LoadItems()
    {
        foreach (Transform child in itemRoot.transform)
        {
            Item newItem = child.gameObject.GetComponent<Item>();
            items.Add(newItem);
        }
        Shuffle(items);
    }

    void LoadFlags()
    {
        foreach (Transform child in flagRoot.transform)
        {
            Flag newFlag = child.gameObject.GetComponent<Flag>();
            flags.Add(newFlag);
        }
    }

    void LoadPlatforms()
    {
        int count = 0;
        foreach (Transform child in platformRoot.transform)
        {
            items[count].objects[count].SetActive(true);
            Platform newPlatform = child.gameObject.GetComponent<Platform>();
            newPlatform.SetItem(items[count++]);
            platforms.Add(newPlatform);
        }
    }

    // Fisher-Yates Shuffle
    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randIndex = Random.Range(0, i + 1);  // UnityEngine.Random
            T temp = list[i];
            list[i] = list[randIndex];
            list[randIndex] = temp;
        }
    }

    public void CheckResults()
    {
        foreach (Platform p in platforms)
        {
            Debug.Log("Platform - " + p.IsPlatformCorrect().ToString());
        }
    }

    void Start()
    {
        LoadItems();
        LoadFlags();
        LoadPlatforms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
