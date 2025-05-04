using UnityEngine;

public class EllaTalkTrigger : MonoBehaviour
{
    [Header("Main script")]
    public MainScript mainScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainScript.StartEllaTalk();
            Destroy(gameObject);
        }   
    }
}
