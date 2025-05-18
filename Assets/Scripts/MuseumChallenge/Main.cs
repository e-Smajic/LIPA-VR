using UnityEngine;
using System.Collections.Generic;
using TMPro;

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

    [Header("Timer")]
    public TMP_Text timerText;
    private float timer;
    private bool isCountingTime = false;
    private bool isLevelStarted = false; 

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
            Platform newPlatform = child.gameObject.GetComponent<Platform>();
            newPlatform.SetItem(items[count]);
            //newPlatform.RemoveHideCapsule();
            platforms.Add(newPlatform);

            //Tablet newTablet = newPlatform.transform.GetComponentInChildren<Tablet>();
            //newTablet.name.text = "Name: " + items[count].name;
            //newTablet.description.text = "Description:\n" + items[count].description;

            count++;
        }
    }

    void LoadGame()
    {
        int count = 0;
        foreach (Platform platform in platforms)
        {
            items[count].objects[count].SetActive(true);
            platform.RemoveHideCapsule();
            Tablet newTablet = platform.transform.GetComponentInChildren<Tablet>();
            newTablet.name.text = "Name: " + items[count].name;
            newTablet.description.text = "Description:\n" + items[count].description;
            count++;
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
            if (p.IsPlatformCorrect())
            {
                p.AddCorrectCapsule();
            }
            else
            {
                p.AddIncorrectCapsule();
            }
        }
    }

    private bool IsSingleDigit(int number)
    {
        return (number / 10) == 0;
    }

    public void ChangeLevelState()
    {
        if (!isLevelStarted)
        {
            StartLevel();
        }
        else
        {
            isCountingTime = false;
            CheckResults();
        }
    }

    public void StartLevel()
    {
        isCountingTime = true;
        isLevelStarted = true;
        LoadGame();
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
        if (isCountingTime)
        {
            timer += Time.deltaTime; // Add time since last frame
            int seconds = Mathf.FloorToInt(timer); // Round down to whole seconds
            int minutes = seconds / 60;
            if (seconds >= 60)
            {
                seconds = seconds % 60;
            }

            if (minutes >= 59 && seconds >= 59)
            {
                isCountingTime = false;
            }

            timerText.text = (IsSingleDigit(minutes) ? "0" + minutes.ToString() : minutes.ToString())
                            + ":" + (IsSingleDigit(seconds) ? "0" + seconds.ToString() : seconds.ToString());
        }
    }
}
