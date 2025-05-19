using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MainScript : MonoBehaviour
{
    [Header("Ella Script")]
    public Ella ella;
    public GameObject dialogUI;
    public TMP_Text dialogText;
    public GameObject continueButton;
    public GameObject yesButton;
    public GameObject noButton;

    [Header("Player")]
    public GameObject locomotion;
    public GameObject vignette;

    // Shopping List:
    [Header("Shopping list")]
    public TMP_Text shoppingListText;
    public ShoppingList shoppingList;

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

    // Tasks:
    private bool talkedToElla = false;
    private bool levelFailed = false;
    private bool levelPassed = false;

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
                    levelFailed = true;
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
        }
    }
}
