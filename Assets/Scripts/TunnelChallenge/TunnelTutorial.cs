using UnityEngine;
using TMPro;
using System.Collections;

public class TunnelTutorial : MonoBehaviour
{
    [Header("Player")]
    public Minecart minecart;
    public TC_Main mainScript;

    [Header("Ella")]
    public Animator ellaAnimator;
    public string[] ellaLines = 
    {
        "Do you need help?",
        "You are in a wagon with a lever in front of you.",
        "On your right, there is a display with questions.",
        "If you get it right, you will go through.",
        "Get all questions right, in order to win.",
        "When you are ready to start, press Continue."
    };
    private int currentEllaLine = 0;
    public GameObject ellaDialogUI;
    public TMP_Text ellaDialogText;
    public GameObject ellaYesButton;
    public GameObject ellaNoButton;
    public GameObject ellaContinueButton;

    [Header("Danny")]
    public Animator dannyAnimator;
    public string[] dannyLines = 
    {
        "This is the Tunnel Challenge.",
        "Use that lever to steer the wagon.",
        "Choose the correct answer by going through the tunnel with its color.",
        "If you get it wrong, you will fall into the void.",
        "Also, avoid walls or you will crash the wagon."
    };
    private int currentDannyLine = 0;
    public GameObject dannyDialogUI;
    public TMP_Text dannyDialogText;
    public GameObject dannyYesButton;
    public GameObject dannyNoButton;
    public GameObject dannyContinueButton;

    private bool talkerFlag = false;     // false - Ella,    true - Danny
    private bool tutorialFinished = false;

    public void StartDannyTalk()
    {
        dannyAnimator.SetBool("IsTalking", true);
        dannyDialogUI.SetActive(true);
    }

    public void StopDannyTalk()
    {
        dannyAnimator.SetBool("IsTalking", false);
        dannyDialogUI.SetActive(false);
    }

    public void StartEllaTalk()
    {
        ellaAnimator.SetBool("IsTalking", true);
        ellaDialogUI.SetActive(true);
    }

    public void StopEllaTalk()
    {
        ellaAnimator.SetBool("IsTalking", false);
        ellaDialogUI.SetActive(false);
    }

    IEnumerator TypeText(TMP_Text dialogText, GameObject yesButton, GameObject noButton, GameObject continueButton, string message, bool yesNoQuestion)
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

    public void ChangeTalker()
    {
        Debug.Log("Talker changed!");
        talkerFlag = !talkerFlag;
    }

    public void MoveDialog(int offset)
    {
        if (!tutorialFinished)
        {
            Debug.Log("TF: " + talkerFlag);
            Debug.Log("ECL: " + currentEllaLine);
            Debug.Log("DCL: " + currentDannyLine);
            ellaYesButton.SetActive(false);
            ellaNoButton.SetActive(false);
            ellaContinueButton.SetActive(false);
            dannyYesButton.SetActive(false);
            dannyNoButton.SetActive(false);
            dannyContinueButton.SetActive(false);

            if (!talkerFlag)
                currentEllaLine += offset;
            else
                currentDannyLine += offset;

            if (currentEllaLine >= 5)
            {
                tutorialFinished = true;
            }  

            if (!talkerFlag && currentEllaLine == 0)
            {
                StartEllaTalk();
                StopDannyTalk();
                StartCoroutine(TypeText(ellaDialogText, ellaYesButton, ellaNoButton, ellaContinueButton, ellaLines[currentEllaLine], true));
            }
            else if (!talkerFlag)
            {
                StartEllaTalk();
                StopDannyTalk();
                StartCoroutine(TypeText(ellaDialogText, ellaYesButton, ellaNoButton, ellaContinueButton, ellaLines[currentEllaLine], false));
            }
            else
            {
                StopEllaTalk();
                StartDannyTalk();
                StartCoroutine(TypeText(dannyDialogText, dannyYesButton, dannyNoButton, dannyContinueButton, dannyLines[currentDannyLine], false));
            }
        }
        else
        {
            StopEllaTalk();
            StopDannyTalk();
            mainScript.UpdateQuestion();
            minecart.isMoving = true;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveDialog(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
