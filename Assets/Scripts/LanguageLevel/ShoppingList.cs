using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ShoppingList : MonoBehaviour
{
    const int NO_OF_DRINKS = 2;
    const int NO_OF_SNACKS = 3;
    const int NO_OF_SWEETS = 2;
    const int NO_OF_CAKES = 1;

    private List<string> drinks = new List<string>
    {
        "Cola", "Orangensaft", "Apfelsaft", "Traubensaft", "Erdbeersaft"
    };
    private List<string> snacks = new List<string>
    {
        "gesalzene Chips", "Paprikachips", "Popcorn", "Bretzeln", "Erdnüsse", "gesaltzene Cracker", "Käsecracker"
    };
    private List<string> sweets = new List<string>
    {
        "Schokolade", "Gummibärchen", "Kekse"
    };
    private List<string> cakes = new List<string>
    {
        "Schokoladenkuchen", "Vanillekuchen", "Obstkuchen"
    };

    public List<string> items = new List<string>();
    public List<string> boughtItems = new List<string>();

    private string TakeRandomElementFromList(List<string> list)
    {
        int index = Random.Range(0, list.Count);
        string item = list[index];
        list.RemoveAt(index);
        return item;
    }

    public bool IsEverythingBought()
    {
        return new HashSet<string>(items).SetEquals(boughtItems);
    }

    private void AddDrinks(List<string> list)
    {
        for (int i = 0; i < NO_OF_DRINKS; i++)
        {
            string newElement = TakeRandomElementFromList(drinks);
            list.Add(newElement);
        }
    }

    private void AddSnacks(List<string> list)
    {
        for (int i = 0; i < NO_OF_SNACKS; i++)
        {
            string newElement = TakeRandomElementFromList(snacks);
            list.Add(newElement);
        }
    }

    private void AddSweets(List<string> list)
    {
        for (int i = 0; i < NO_OF_SWEETS; i++)
        {
            string newElement = TakeRandomElementFromList(sweets);
            list.Add(newElement);
        }
    }

    private void AddCakes(List<string> list)
    {
        for (int i = 0; i < NO_OF_CAKES; i++)
        {
            string newElement = TakeRandomElementFromList(cakes);
            list.Add(newElement);
        }
    }
    
    public void GenerateList()
    {
        AddDrinks(items);
        AddSnacks(items);
        AddSweets(items);
        AddCakes(items);
    }
}
