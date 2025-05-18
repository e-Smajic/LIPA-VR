using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

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
    private int currentLine = 0;
    private bool talkedToDanny = false;

    [Header("Player")]
    public GameObject locomotion;
    public GameObject vignette;
    public NearFarInteractor leftHandNFI;
    public NearFarInteractor rightHandNFI;

    [Header("UI")]
    public GameObject dialogUI;
    public TMP_Text dialogText;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject continueButton;

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
            vignette.SetActive(true);
        }
    }
}
