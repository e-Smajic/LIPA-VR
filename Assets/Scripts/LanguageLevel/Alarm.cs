using UnityEngine;

public class Alarm : MonoBehaviour
{
    public MainScript mainScript;

    void OnTriggerEnter(Collider other)
    {
        ShoppingItem item = other.gameObject.GetComponent<ShoppingItem>();
        if (item)
            mainScript.SetAlarmOn();
    }
}
