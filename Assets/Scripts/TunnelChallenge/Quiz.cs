using UnityEngine;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{
    public string name;
    public List<Question> questions = new List<Question>();
    public int length = 5;

    // Fisher-Yates Shuffle
    void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randIndex = Random.Range(0, i + 1);  // UnityEngine.Random
            T temp = list[i];
            list[i] = list[randIndex];
            list[randIndex] = temp;
        }
    }

    void Start()
    {
        Shuffle(questions);
    }
}
