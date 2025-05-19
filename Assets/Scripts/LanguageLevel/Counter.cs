using UnityEngine;

public class Counter : MonoBehaviour
{
    public ShoppingList shoppingList;

    void OnTriggerEnter(Collider other)
    {
        ShoppingItem item = other.GetComponent<ShoppingItem>();
        if (item)
        {
            shoppingList.boughtItems.Add(item.name);
            Debug.Log("Entered: " + item.name);
            Debug.Log("Lists are the same: " + shoppingList.IsEverythingBought());
        }     
    }

    void OnTriggerExit(Collider other)
    {
        ShoppingItem item = other.GetComponent<ShoppingItem>();
        if (item)
        {
            shoppingList.boughtItems.Remove(item.name);
            Debug.Log("Exited: " + item.name);
            Debug.Log("Lists are the same: " + shoppingList.IsEverythingBought());
        }
    }
}
