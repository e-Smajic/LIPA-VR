using UnityEngine;
using System.Collections.Generic;

public class Platform : MonoBehaviour
{
    private List<Flag> choices = new List<Flag>();
    private Item item = null;

    public void OnCollisionEnter(Collision other)
    {
        Flag current = other.gameObject.GetComponent<Flag>();

        if (current)
        {
            choices.Add(current);
            Debug.Log(choices.Count);
            Debug.Log(current.country);
            Debug.Log(item.country);
        }
    }

    public void OnCollisionExit(Collision other)
    {
        Flag current = other.gameObject.GetComponent<Flag>();

        if (current)
        {
            choices.Remove(current);
            Debug.Log(choices.Count);
            Debug.Log(current.country);
            Debug.Log(item.country);
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
}
