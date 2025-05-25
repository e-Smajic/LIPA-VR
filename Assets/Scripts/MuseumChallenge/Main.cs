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
    private bool allCorrect = true; 

    [Header("Danny")]
    public DannyScript dannyScript;

    [Header("Highscores")]
    public TMP_Text highscoreText;

    [Header("Sounds")]
    public AudioSource bgm;
    public AudioSource victorySound;
    public AudioSource defeatSound;

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
            platforms.Add(newPlatform);
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

    void LoadHighscores()
    {
        string firstName = PlayerPrefs.GetString("MuseumFirstName", "Empty");
        string secondName = PlayerPrefs.GetString("MuseumSecondName", "Empty");
        string thirdName = PlayerPrefs.GetString("MuseumThirdName", "Empty");

        string firstTime = PlayerPrefs.GetString("MuseumFirstTime", "");
        string secondTime = PlayerPrefs.GetString("MuseumSecondTime", "");
        string thirdTime = PlayerPrefs.GetString("MuseumThirdTime", "");

        string final = "Best times:\n" + firstName + "   " + "(" + firstTime + ")\n"
                                       + secondName + "   " + "(" + secondTime + ")\n"
                                       + thirdName + "   " + "(" + thirdTime + ")\n";

        highscoreText.text = final;
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
            if (p.IsPlatformCorrect())
            {
                p.AddCorrectCapsule();
            }
            else
            {
                p.AddIncorrectCapsule();
                allCorrect = false;
            }
        }

        if (allCorrect)
        {
            bgm.Stop();
            victorySound.Play();

            float thirdTimer = PlayerPrefs.GetFloat("MuseumThirdTimer", float.MaxValue);
            if (timer < thirdTimer)
            {
                dannyScript.StartHighscoreDialog(timer, timerText.text);
            }
        }
        else
        {
            bgm.Stop();
            defeatSound.Play();
            dannyScript.StartDefeatDialog();
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
        LoadHighscores();
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
