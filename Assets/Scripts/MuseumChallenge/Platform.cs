using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platform : MonoBehaviour
{
    private List<Flag> choices = new List<Flag>();
    private Item item = null;

    [Header("Capsules")]
    public MeshRenderer hideCapsule;
    public MeshRenderer correctCapsule;
    public MeshRenderer incorrectCapsule;

    public void OnCollisionEnter(Collision other)
    {
        Flag current = other.gameObject.GetComponent<Flag>();

        if (current)
        {
            choices.Add(current);
        }
    }

    public void OnCollisionExit(Collision other)
    {
        Flag current = other.gameObject.GetComponent<Flag>();

        if (current)
        {
            choices.Remove(current);
        }
    }

    public bool IsPlatformCorrect()
    {
        if (choices.Count == 1 && choices[0].country.Equals(item.country))
            return true;
        return false;
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public Item GetItem()
    {
        return this.item;
    }

    public void RemoveHideCapsule()
    {
        StartCoroutine(FadeAlpha(hideCapsule.material, 0, 1f));
    }

    public void AddCorrectCapsule()
    {
        correctCapsule.enabled = true;
        StartCoroutine(FadeAlpha(correctCapsule.material, 0.3f, 1f));
    }

    public void AddIncorrectCapsule()
    {
        incorrectCapsule.enabled = true;
        StartCoroutine(FadeAlpha(incorrectCapsule.material, 0.3f, 1f));
    }

    IEnumerator FadeAlpha(Material mat, float alpha, float duration)
    {
        float startAlpha = mat.color.a;
        float time = 0f;

        while (time < duration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, alpha, time / duration);
            Color c = mat.color;
            c.a = newAlpha;
            mat.color = c;

            time += Time.deltaTime;
            yield return null;
        }

        Color finalColor = mat.color;
        finalColor.a = alpha;
        mat.color = finalColor;
    }
}
