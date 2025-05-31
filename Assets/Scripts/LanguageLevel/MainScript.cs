using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class MainScript : MonoBehaviour
{
    [Header("Ella Script")]
    public Ella ella;
    public GameObject dialogUI;
    public TMP_Text dialogText;
    public GameObject continueButton;
    public GameObject yesButton;
    public GameObject noButton;

    [Header("Shopping assistant")]
    public ShopAssistant shopAssistant;
    public GameObject shopDialogUI;
    public TMP_Text shopDialogText;
    public GameObject shopContinueButton;
    public GameObject shopYesButton;
    public GameObject shopNoButton;

    [Header("Player")]
    public GameObject locomotion;
    public GameObject vignette;
    public NearFarInteractor leftHandNFI;
    public NearFarInteractor rightHandNFI;

    [Header("Shopping list")]
    public TMP_Text shoppingListText;
    public ShoppingList shoppingList;

    [Header("BGM")]
    public AudioSource bgm;
    public AudioSource victorySound;
    public AudioSource defeatSound;
    public AudioSource alarmSound;

    // Ella Lines:
    private string[] ellaLines = 
    {
        "Heute ist Dannys Geburtstag.",
        "Ich möchte eine Überraschungsparty veranstalten.",
        "Kannst du einige Snacks kaufen?",
        "Oh. Dann werde ich jemand anderen finden.",
        "Super. Die Liste ist auf dem Tisch."
    };
    private int currentLine = 0;

    private string[] shopAssistantLines = 
    {
        "Brauchen Sie noch etwas?",
        "Vielen Dank. Tschüss!",
        "Sind Sie sicher? Überprüfen Sie Ihre Liste noch einmal."
    };

    // Tasks:
    private bool talkedToElla = false;
    public bool levelPassed = false;

    void Start()
    {
        RenderShoppingList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RenderShoppingList()
    {
        shoppingList.GenerateList();
        
        string listText = "Einkaufsliste:\n\n";
        
        foreach(string item in shoppingList.items)
            listText += "- " + item + "\n";

        shoppingListText.text = listText;
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

    public void StartEllaTalk()
    {
        dialogUI.SetActive(true);
        ella.StartTalking();
        locomotion.SetActive(false);
        vignette.SetActive(false);
        leftHandNFI.enableFarCasting = true;
        rightHandNFI.enableFarCasting = true;
        MoveDialog(0);
    }

    public void MoveDialog(int offset)
    {
        if (!talkedToElla)
        {
            yesButton.SetActive(false);
            noButton.SetActive(false);
            continueButton.SetActive(false);

            currentLine += offset;

            if (currentLine >= 3)
            {
                talkedToElla = true;
                
                if (currentLine == 3)
                {
                    bgm.Stop();
                    defeatSound.Play();
                    StartCoroutine(LoadSceneAfterDelay(7f));
                }
            }

            if (currentLine == 2)
                StartCoroutine(TypeText(ellaLines[currentLine], true));
            else
                StartCoroutine(TypeText(ellaLines[currentLine], false));
        }
        else
        {
            dialogUI.SetActive(false);
            ella.StopTalking();
            locomotion.SetActive(true);
            vignette.SetActive(true);
            leftHandNFI.enableFarCasting = false;
            rightHandNFI.enableFarCasting = false;
        }
    }

    public void SetAlarmOn()
    {
        bgm.Stop();
        alarmSound.Play();
        StartCoroutine(LoadSceneAfterDelay(5f));
    }

    public void AskFinish()
    {
        locomotion.SetActive(false);
        vignette.SetActive(false);
        leftHandNFI.enableFarCasting = true;
        rightHandNFI.enableFarCasting = true;
        shopAssistant.StartTalking();
        shopDialogUI.SetActive(true);
        shopDialogText.text = shopAssistantLines[0];
        shopNoButton.SetActive(true);
        shopYesButton.SetActive(true);
        shopContinueButton.SetActive(false);
    }

    public void AnswerFinishPositive()
    {
        locomotion.SetActive(true);
        vignette.SetActive(true);
        leftHandNFI.enableFarCasting = false;
        rightHandNFI.enableFarCasting = false;
        shopAssistant.StopTalking();
        shopDialogUI.SetActive(false);
        shopNoButton.SetActive(false);
        shopYesButton.SetActive(false);
        shopContinueButton.SetActive(false);
    }

    public void AnswerFinishNegative()
    {
        shopNoButton.SetActive(false);
        shopYesButton.SetActive(false);

        if (levelPassed)
        {
            shopDialogText.text = shopAssistantLines[1];
            bgm.Stop();
            victorySound.Play();
            StartCoroutine(LoadSceneAfterDelay(10f));
        }
        else
        {
            shopDialogText.text = shopAssistantLines[2];
            bgm.Stop();
            defeatSound.Play();
            StartCoroutine(LoadSceneAfterDelay(10f));
        }
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MenuScene");
    }
}
