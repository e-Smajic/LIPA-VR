using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TC_Main : MonoBehaviour
{
    [Header("Quiz")]
    public Quiz quiz;
    private int currentQuestion = 0;

    [Header("Tunnels")]
    public Transform world;
    public GameObject tunnel;
    public GameObject wall;
    private Vector3 startPosition = Vector3.zero;
    private List<int> correctPaths = new List<int>();

    [Header("Interface")]
    public TMP_Text questionText;
    public TMP_Text blueAnswerText;
    public TMP_Text greenAnswerText;
    public TMP_Text redAnswerText;

    void GenerateTunnels()
    {
        for (int i = 0; i < quiz.length; i++)
        {
            Vector3 spawnPosition = new Vector3(0, 4, -190 * i);
            GameObject newTunnel = Instantiate(tunnel, spawnPosition, Quaternion.identity, world);
            newTunnel.SetActive(true);
            
            correctPaths.Add(Random.Range(0, 3));
            Transform triggerTransform = newTunnel.transform.GetChild(1).GetChild(correctPaths[i]).Find("Trigger");

            if (triggerTransform)
            {
                TunnelChoice tunnelChoice = triggerTransform.GetComponent<TunnelChoice>();
                if (tunnelChoice)   
                    tunnelChoice.isCorrect = true;
            }
        }

        Vector3 lastPos = new Vector3(0, 4, -190 * quiz.length);
        GameObject lastTunnel = Instantiate(tunnel, lastPos, Quaternion.identity, world);
        lastTunnel.SetActive(true);
        Vector3 lastPosWall = new Vector3(0, 4, -190 * quiz.length - 50);
        GameObject lastWall = Instantiate(wall, lastPosWall, Quaternion.identity, world);
    }

    void UpdateQuestion()
    {
        questionText.text = quiz.questions[currentQuestion].questionText;

        if (correctPaths[currentQuestion] == 0)
        {
            blueAnswerText.text = quiz.questions[currentQuestion].correctAnswer;
            greenAnswerText.text = quiz.questions[currentQuestion].incorrectAnswers[0];
            redAnswerText.text = quiz.questions[currentQuestion].incorrectAnswers[1];
        }
        else if (correctPaths[currentQuestion] == 1)
        {
            blueAnswerText.text = quiz.questions[currentQuestion].incorrectAnswers[0];
            greenAnswerText.text = quiz.questions[currentQuestion].correctAnswer;
            redAnswerText.text = quiz.questions[currentQuestion].incorrectAnswers[1];
        }
        else
        {
            blueAnswerText.text = quiz.questions[currentQuestion].incorrectAnswers[0];
            greenAnswerText.text = quiz.questions[currentQuestion].incorrectAnswers[1];
            redAnswerText.text = quiz.questions[currentQuestion].correctAnswer;
        }
    }

    public void NextQuestion()
    {
        currentQuestion++;
        if (currentQuestion < quiz.length)
        {
            UpdateQuestion();
        }
    }
    
    void Start()
    {
        GenerateTunnels();
        UpdateQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
