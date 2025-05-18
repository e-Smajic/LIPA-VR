using UnityEngine;

public class CrashChecker : MonoBehaviour
{
    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("Crash " + other.gameObject.tag);
    }
}
