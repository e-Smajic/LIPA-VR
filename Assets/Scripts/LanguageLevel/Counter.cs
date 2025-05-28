using UnityEngine;

public class Counter : MonoBehaviour
{
    public ShoppingList shoppingList;
    public MainScript mainScript;

    void OnTriggerEnter(Collider other)
    {
        ShoppingItem item = other.GetComponent<ShoppingItem>();
        if (item)
        {
            shoppingList.boughtItems.Add(item.name);
            mainScript.levelPassed = shoppingList.IsEverythingBought();
            mainScript.AskFinish();
        }     
    }

    void OnTriggerExit(Collider other)
    {
        ShoppingItem item = other.GetComponent<ShoppingItem>();
        if (item)
        {
            shoppingList.boughtItems.Remove(item.name);
            mainScript.levelPassed = shoppingList.IsEverythingBought();
        }
    }
}
