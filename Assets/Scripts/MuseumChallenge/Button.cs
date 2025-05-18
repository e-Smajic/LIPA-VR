using UnityEngine;

public class Button : MonoBehaviour
{
    public Main mainScript;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Button"))
        {
            mainScript.ChangeLevelState();
        }
    }
}
