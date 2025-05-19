using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("Main Menu")]
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

    [Header("Book")]
    public Animator bookAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        StartCoroutine(FadeAlpha(0f, 1f, 1f));
    }

    IEnumerator WaitForBookClose()
    {
        yield return new WaitForSeconds(1f);
        mainMenuParent.SetActive(false);
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
}
