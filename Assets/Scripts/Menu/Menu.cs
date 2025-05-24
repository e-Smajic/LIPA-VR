using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject logoParent;
    public Image logoImage;
    public TMP_Text appName;
    public TMP_Text signature;
    public GameObject mainMenuParent;
    public GameObject learnButton;
    public TMP_Text learnText;
    public Image learnIcon;
    public GameObject settingsButton;
    public TMP_Text settingsText;
    public Image settingsIcon;
    public GameObject exitButton;
    public TMP_Text exitText;
    public Image exitIcon;

    [Header("Learn Menu")]
    public GameObject learnMenuParent;

    [Header("Museum Menu")]
    public GameObject museumMenuParent;

    [Header("Language Menu")]
    public GameObject languageMenuParent;

    [Header("Tunnel Menu")]
    public GameObject tunnelMenuParent;

    [Header("Settings Menu")]
    public GameObject settingsMenuParent;
    public Slider audioSlider;
    public TMP_Dropdown movementType;
    public Slider movementSpeed;
    public TMP_Text movementSpeedValue;
    public TMP_Dropdown turnType;
    public TMP_Dropdown turnAngle;
    public Toggle tunnelingToggle;
    public Text tunnelingLabel;

    [Header("Loading Menu")]
    public GameObject loadingParent;
    public Slider loadingSlider;

    [Header("Book")]
    public Animator bookAnimator;

    // Scene Names:
    private string museumLevel = "MuseumScene";
    private string languageLevel = "LanguageLevelScene";
    private string tunnelLevel = "TunnelChallengeScene";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadVolume();
        LoadMovementType();
        LoadMovementSpeed();
        LoadTurnType();
        LoadTurnAngle();
        LoadTunneling();
        StartCoroutine(WaitForBookOpening());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLearnMenu()
    {
        mainMenuParent.SetActive(false);
        learnMenuParent.SetActive(true);
    }

    public void CloseLearnMenu()
    {
        mainMenuParent.SetActive(true);
        learnMenuParent.SetActive(false);
    }

    public void OpenMuseumMenu()
    {
        learnMenuParent.SetActive(false);
        museumMenuParent.SetActive(true);
    }

    public void CloseMuseumMenu()
    {
        learnMenuParent.SetActive(true);
        museumMenuParent.SetActive(false);
    }

    public void OpenLanguageMenu()
    {
        learnMenuParent.SetActive(false);
        languageMenuParent.SetActive(true);
    }

    public void CloseLanguageMenu()
    {
        learnMenuParent.SetActive(true);
        languageMenuParent.SetActive(false);
    }

    public void OpenTunnelMenu()
    {
        learnMenuParent.SetActive(false);
        tunnelMenuParent.SetActive(true);
    }

    public void CloseTunnelMenu()
    {
        learnMenuParent.SetActive(true);
        tunnelMenuParent.SetActive(false);
    }

    public void OpenSettingsMenu()
    {
        mainMenuParent.SetActive(false);
        settingsMenuParent.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        mainMenuParent.SetActive(true);
        settingsMenuParent.SetActive(false);
    }

    public void LoadVolume()
    {
        audioSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
    }

    public void UpdateVolume()
    {
        float volume = Mathf.Clamp(audioSlider.value, 0, 1);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void LoadMovementType()
    {
        movementType.value = PlayerPrefs.GetInt("MovementType", 0);
    }

    public void UpdateMovementType()
    {
        int selected = movementType.value;
        PlayerPrefs.SetInt("MovementType", selected);
        PlayerPrefs.Save();
    }

    public void LoadMovementSpeed()
    {
        float speed = PlayerPrefs.GetFloat("MovementSpeed", 2.5f);
        speed = Mathf.Round(speed * 10f) / 10f;
        movementSpeed.value = speed;
        movementSpeedValue.text = speed.ToString();
    }

    public void UpdateMovementSpeed()
    {
        float speed = Mathf.Clamp(movementSpeed.value, 1, 10);
        speed = Mathf.Round(speed * 10f) / 10f;
        PlayerPrefs.SetFloat("MovementSpeed", speed);
        movementSpeedValue.text = speed.ToString();
    }

    public void LoadTurnType()
    {
        turnType.value = PlayerPrefs.GetInt("TurnType", 0);
    }

    public void UpdateTurnType()
    {
        int selected = turnType.value;
        PlayerPrefs.SetInt("TurnType", selected);
        PlayerPrefs.Save();
    }

    public void LoadTurnAngle()
    {
        turnAngle.value = PlayerPrefs.GetInt("TurnAngle", 2);
    }

    public void UpdateTurnAngle()
    {
        int selected = turnAngle.value;
        PlayerPrefs.SetInt("TurnAngle", selected);
        PlayerPrefs.Save();
    }

    public void LoadTunneling()
    {
        bool isEnabled = PlayerPrefs.GetInt("Tunneling", 1) == 1;
        tunnelingToggle.isOn = isEnabled;
        tunnelingLabel.text = (isEnabled) ? "Enabled" : "Disabled";
    }

    public void UpdateTunneling()
    {
        bool isEnabled = tunnelingToggle.isOn;
        tunnelingLabel.text = (isEnabled) ? "Enabled" : "Disabled";
        int tunneling = isEnabled ? 1 : 0;
        PlayerPrefs.SetInt("Tunneling", tunneling);
        PlayerPrefs.Save();
    }

    public void Quit()
    {
        StartCoroutine(FadeAlpha(1f, 0f, 1f));
        StartCoroutine(WaitForBookClose());
    }

    IEnumerator QuitApp()
    {
        yield return new WaitForSeconds(1f);
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    IEnumerator WaitForBookOpening()
    {
        yield return new WaitForSeconds(1f);
        mainMenuParent.SetActive(true);
        logoParent.SetActive(true);
        StartCoroutine(FadeAlpha(0f, 1f, 1f));
    }

    IEnumerator WaitForBookClose()
    {
        yield return new WaitForSeconds(1f);
        mainMenuParent.SetActive(false);
        logoParent.SetActive(false);
        bookAnimator.SetBool("CloseApp", true);
        StartCoroutine(QuitApp());
    }

    IEnumerator FadeAlpha(float start, float end, float fadeDuration)
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            Image learnButtonBackground = learnButton.GetComponent<Image>();
            Image settingsButtonBackground = settingsButton.GetComponent<Image>();
            Image exitButtonBackground = exitButton.GetComponent<Image>();

            float a = Mathf.Lerp(start, end, time / fadeDuration);
            if (logoImage != null) logoImage.color = new Color(logoImage.color.r, logoImage.color.g, logoImage.color.b, a);
            if (appName != null) appName.color = new Color(appName.color.r, appName.color.g, appName.color.b, a);
            if (signature != null) signature.color = new Color(signature.color.r, signature.color.g, signature.color.b, a);

            if (learnIcon != null) learnIcon.color = new Color(learnIcon.color.r, learnIcon.color.g, learnIcon.color.b, a);
            if (learnText != null) learnText.color = new Color(learnText.color.r, learnText.color.g, learnText.color.b, a);
            if (learnButtonBackground != null) learnButtonBackground.color = new Color(learnButtonBackground.color.r, learnButtonBackground.color.g, learnButtonBackground.color.b, a);

            if (settingsIcon != null) settingsIcon.color = new Color(settingsIcon.color.r, settingsIcon.color.g, settingsIcon.color.b, a);
            if (settingsText != null) settingsText.color = new Color(settingsText.color.r, settingsText.color.g, settingsText.color.b, a);
            if (settingsButtonBackground != null) settingsButtonBackground.color = new Color(settingsButtonBackground.color.r, settingsButtonBackground.color.g, settingsButtonBackground.color.b, a);

            if (exitIcon != null) exitIcon.color = new Color(exitIcon.color.r, exitIcon.color.g, exitIcon.color.b, a);
            if (exitText != null) exitText.color = new Color(exitText.color.r, exitText.color.g, exitText.color.b, a);
            if (exitButtonBackground != null) exitButtonBackground.color = new Color(exitButtonBackground.color.r, exitButtonBackground.color.g, exitButtonBackground.color.b, a);

            time += Time.deltaTime;
            yield return null;
        }
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName.Equals(museumLevel))
        {
            museumMenuParent.SetActive(false);
        }
        else if (sceneName.Equals(languageLevel))
        {
            languageMenuParent.SetActive(false);
        }
        else if (sceneName.Equals(tunnelLevel))
        {
            tunnelMenuParent.SetActive(false);
        }

        loadingParent.SetActive(true);

        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName) {
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);

        while(!load.isDone) 
        {
            float progress = Mathf.Clamp01(load.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }
}
