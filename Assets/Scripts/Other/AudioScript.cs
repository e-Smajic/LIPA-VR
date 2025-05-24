using UnityEngine;

public class AudioScript : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (audioSource != null)
            audioSource.volume = PlayerPrefs.GetFloat("Volume", 1f);
    }
}
