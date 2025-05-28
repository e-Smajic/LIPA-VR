using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CrashChecker : MonoBehaviour
{
    public GameObject quad;
    public AudioSource bgm;
    public AudioSource crashSFX;

    IEnumerator FadeAlpha(float start, float end, float fadeDuration)
    {
        Renderer renderer = quad.GetComponent<Renderer>();
        float time = 0f;
        while (time < fadeDuration)
        {
            float a = Mathf.Lerp(start, end, time / fadeDuration);
            if (renderer != null) renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, a);
            time += Time.deltaTime;
            yield return null;
        }
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
        StartCoroutine(LoadSceneAfterDelay(7f));
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MenuScene");
    }

    public void OnCollisionEnter(Collision other)
    {
        if (!quad.activeSelf)
        {
            bgm.Stop();
            crashSFX.Play();
            quad.SetActive(true);
            StartCoroutine(FadeAlpha(0f, 1f, 0.5f));
        }
    }
}
