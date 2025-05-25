using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VirtualKeyboard : MonoBehaviour
{
    public TMP_InputField inputField;

    public void HandleKey(string key)
    {
        if (inputField != null)
        {
            if (key.Equals("<--"))
            {
                if (inputField.text.Length > 0)
                    inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
            }
            else
            {
                inputField.text += key;
            }

            inputField.ActivateInputField();
        }
    }
}
