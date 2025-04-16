using UnityEngine;

public class Platform : MonoBehaviour
{
    private Flag choice = null;
    private Item item = null;

    public void OnCollisionEnter(Collision other)
    {
        Flag current = other.gameObject.GetComponent<Flag>();

        if (current)
        {
            choice = current;
            Debug.Log(choice.country);
            Debug.Log(item.country);
            if (choice.country.Equals(item.country))
                Debug.Log("Correct");
            else
                Debug.Log("Incorrect");
        }
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
