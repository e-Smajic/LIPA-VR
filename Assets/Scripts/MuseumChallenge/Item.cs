using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Main Information")]
    public string name;
    public string country;
    public string description;

    [Header("Prefabs")]
    public GameObject[] objects = new GameObject[8];
}
