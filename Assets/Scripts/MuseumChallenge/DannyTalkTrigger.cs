using UnityEngine;

public class DannyTalkTrigger : MonoBehaviour
{
    [Header("Danny script")]
    public DannyScript mainScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            mainScript.StartDannyTalk();
            Destroy(gameObject);
        }   
    }
}
