using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.SceneManagement;

public class DannyScript : MonoBehaviour
{
    [Header("Animator")]
    public Animator dannyAnimator;
    private string[] dannyLines = 
    {
        "Do you need help?",
        "OK. Have fun!",
        "This is the Museum Challenge.",
        "Behind you, there are multiple flags.",
        "You have to match those flags with their corresponding museum exhibits.",
        "There are more flags than exhibits, so some of them are decoys.",
        "You can read each exhibit's description that can help you.",
        "When you want to start the level, press the button on the table.",
        "When you are done, press the button again.",
        "Your time will be displayed on the wall.",
        "That's it. Have fun!"
    };
    private string highscoreLine = "You made the top 3! Do you want to save your time to the scoreboard?";
    private string defeatLine = "Ah, it seems you didn't get all of them correct. Better luck next time!";
    private int currentLine = 0;
    private bool talkedToDanny = false;

    [Header("Player")]
    public GameObject locomotion;
    public GameObject vignette;
    public NearFarInteractor leftHandNFI;
    public NearFarInteractor rightHandNFI;
    private float playerTime = float.MaxValue;
    private string playerTimeString = "";

    [Header("UI")]
    public GameObject dialogUI;
    public TMP_Text dialogText;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject continueButton;
    public GameObject highscoreUI;
    public GameObject highscoreLineTextParent;
    public TMP_Text highscoreLineText;
    public GameObject highscoreYesButton;
    public GameObject highscoreNoButton;
    public GameObject highscoreContinueButton;
    public GameObject highscoreName;
    public TMP_InputField highscoreNameInput;
    public GameObject virtualKeyboard;

    public void StartDannyTalk()
    {
        dannyAnimator.SetBool("IsTalking", true);
        dialogUI.SetActive(true);
        locomotion.SetActive(false);
        vignette.SetActive(false);
        leftHandNFI.enableFarCasting = true;
        rightHandNFI.enableFarCasting = true;
        MoveDialog(0);
    }

    public void StopDannyTalk()
    {
        dannyAnimator.SetBool("IsTalking", false);
        dialogUI.SetActive(false);
        locomotion.SetActive(true);

        if (PlayerPrefs.GetInt("Tunneling", 1) == 1)
            vignette.SetActive(true);

        leftHandNFI.enableFarCasting = false;
        rightHandNFI.enableFarCasting = false;
    }

    IEnumerator TypeText(string message, bool yesNoQuestion)
    {
        dialogText.text = "";
        foreach (char letter in message)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        if (yesNoQuestion)
        {
            yesButton.SetActive(true);
            noButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(true);
        }
    }

    public void MoveDialog(int offset)
    {
        if (!talkedToDanny)
        {
            yesButton.SetActive(false);
            noButton.SetActive(false);
            continueButton.SetActive(false);

            currentLine += offset;

            if (currentLine == 1 || currentLine >= 10)
            {
                talkedToDanny = true;
            }

            if (currentLine == 0)
                StartCoroutine(TypeText(dannyLines[currentLine], true));
            else
                StartCoroutine(TypeText(dannyLines[currentLine], false));
        }
        else
        {
            dialogUI.SetActive(false);
            StopDannyTalk();
            locomotion.SetActive(true);
            if (PlayerPrefs.GetInt("Tunneling", 1) == 1)
                vignette.SetActive(true);
        }
    }

    public void StartDefeatDialog()
    {
        leftHandNFI.enableFarCasting = true;
        rightHandNFI.enableFarCasting = true;
        highscoreUI.SetActive(true);
        highscoreLineTextParent.SetActive(true);
        highscoreLineText.text = defeatLine;
        highscoreContinueButton.SetActive(true);
    }

    public void StartHighscoreDialog(float timer, string timeString)
    {
        leftHandNFI.enableFarCasting = true;
        rightHandNFI.enableFarCasting = true;
        highscoreUI.SetActive(true);
        highscoreLineTextParent.SetActive(true);
        highscoreLineText.text = highscoreLine;
        playerTime = timer;
        playerTimeString = timeString;
        highscoreYesButton.SetActive(true);
        highscoreNoButton.SetActive(true);
    }

    public void ShowHighscoreInput()
    {
        highscoreLineTextParent.SetActive(false);
        highscoreYesButton.SetActive(false);
        highscoreNoButton.SetActive(false);
        highscoreName.SetActive(true);
        highscoreContinueButton.SetActive(true);
        virtualKeyboard.SetActive(true);
    }

    public void FinishLevel(bool rememberHighscore)
    {
        if (rememberHighscore)
            AddNewHighscore();
        highscoreLineText.text = "Goodbye!";
        virtualKeyboard.SetActive(false);
        highscoreLineTextParent.SetActive(true);
        highscoreYesButton.SetActive(false);
        highscoreNoButton.SetActive(false);
        highscoreName.SetActive(false);
        highscoreContinueButton.SetActive(false);
        StartCoroutine(LoadSceneAsync("MenuScene"));
    }

    public void AddNewHighscore()
    {
        if (playerTime == float.MaxValue)
            return;

        if (playerTime < PlayerPrefs.GetFloat("MuseumFirstTimer", float.MaxValue))
        {
            PlayerPrefs.SetString("MuseumThirdName", PlayerPrefs.GetString("MuseumSecondName", "Empty"));
            PlayerPrefs.SetString("MuseumSecondName", PlayerPrefs.GetString("MuseumFirstName", "Empty"));
            PlayerPrefs.SetString("MuseumFirstName", highscoreNameInput.text);

            PlayerPrefs.SetString("MuseumThirdTime", PlayerPrefs.GetString("MuseumSecondTime", ""));
            PlayerPrefs.SetString("MuseumSecondTime", PlayerPrefs.GetString("MuseumFirstTime", ""));
            PlayerPrefs.SetString("MuseumFirstTime", playerTimeString);

            PlayerPrefs.SetFloat("MuseumThirdTimer", PlayerPrefs.GetFloat("MuseumSecondTimer", float.MaxValue));
            PlayerPrefs.SetFloat("MuseumSecondTimer", PlayerPrefs.GetFloat("MuseumFirstTimer", float.MaxValue));
            PlayerPrefs.SetFloat("MuseumFirstTimer", playerTime);
        }
        else if (playerTime < PlayerPrefs.GetFloat("MuseumSecondTimer", float.MaxValue))
        {
            PlayerPrefs.SetString("MuseumThirdName", PlayerPrefs.GetString("MuseumSecondName", "Empty"));
            PlayerPrefs.SetString("MuseumSecondName", highscoreNameInput.text);

            PlayerPrefs.SetString("MuseumThirdTime", PlayerPrefs.GetString("MuseumSecondTime", ""));
            PlayerPrefs.SetString("MuseumSecondTime", playerTimeString);

            PlayerPrefs.SetFloat("MuseumThirdTimer", PlayerPrefs.GetFloat("MuseumSecondTimer", float.MaxValue));
            PlayerPrefs.SetFloat("MuseumSecondTimer", playerTime);
        }
        else 
        {
            PlayerPrefs.SetString("MuseumThirdName", highscoreNameInput.text);
            
            PlayerPrefs.SetString("MuseumThirdTime", playerTimeString);

            PlayerPrefs.SetFloat("MuseumThirdTimer", playerTime);
        }
    }

    IEnumerator LoadSceneAsync(string sceneName) {
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);

        while(!load.isDone) 
        {
            float progress = Mathf.Clamp01(load.progress / 0.9f);
            yield return null;
        }
    }
}
