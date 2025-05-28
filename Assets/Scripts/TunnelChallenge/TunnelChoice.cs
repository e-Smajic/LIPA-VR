using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TunnelChoice : MonoBehaviour
{
    public bool isCorrect = false;
    public GameObject floor;
    public Minecart minecart;
    public TC_Main mainScript;
    private bool isFalling = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (!isCorrect)
            {
                if (!mainScript.quad.activeSelf)
                {
                    mainScript.bgm.Stop();
                    mainScript.defeatSound.Play();
                    mainScript.quad.SetActive(true);
                    isFalling = true;
                    minecart.downSpeed = minecart.forwardSpeed;
                    minecart.forwardSpeed /= 2;
                    minecart.sideSpeed = 0f;
                    StartCoroutine(WaitAndLoadMenu(5f));
                }
            }
            else
            {
                mainScript.NextQuestion();
            }
        }
    }

    IEnumerator FadeA(float start, float end, float fadeDuration)
    {
        Renderer renderer = mainScript.quad.GetComponent<Renderer>();
        float time = 0f;
        while (time < fadeDuration)
        {
            float a = Mathf.Lerp(start, end, time / fadeDuration);
            if (renderer != null) renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, a);
            time += Time.deltaTime;
            yield return null;
        }
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
        SceneManager.LoadScene("MenuScene");
    }

    IEnumerator WaitAndLoadMenu(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(FadeA(0f, 1f, 1f));
    }

    void Update()
    {
        if (isFalling)
            floor.transform.Translate(- Vector3.forward.normalized * minecart.downSpeed * 1.2f * Time.deltaTime);
    }
}
