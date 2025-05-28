using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TC_Main : MonoBehaviour
{
    [Header("Quiz")]
    public Quiz quiz;
    private int currentQuestion = 0;
    private bool isFinished = false;

    [Header("Player")]
    public Minecart minecart;
    public GameObject quad;

    [Header("Tunnels")]
    public Transform world;
    public GameObject tunnel;
    public GameObject wall;
    private Vector3 startPosition = Vector3.zero;
    private List<int> correctPaths = new List<int>();
    private GameObject[] confetti = new GameObject[3];

    [Header("Interface")]
    public TMP_Text questionText;
    public TMP_Text blueAnswerText;
    public TMP_Text greenAnswerText;
    public TMP_Text redAnswerText;

    [Header("BGM")]
    public AudioSource bgm;
    public AudioSource victorySound;
    public AudioSource defeatSound;

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

        confetti[0] = lastTunnel.transform.GetChild(3).gameObject;
        confetti[1] = lastTunnel.transform.GetChild(4).gameObject;
        confetti[2] = lastTunnel.transform.GetChild(5).gameObject;

        Vector3 lastPosWall = new Vector3(0, 4, -190 * quiz.length - 50);
        GameObject lastWall = Instantiate(wall, lastPosWall, Quaternion.identity, world);
    }

    public void UpdateQuestion()
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
        else
        {
            EndQuiz();
        }
    }

    void EndQuiz()
    {
        isFinished = true;
        bgm.Stop();
        victorySound.Play();

        foreach (GameObject c in confetti)
        {
            c.SetActive(true);
        }

        StartCoroutine(LoadSceneAfterDelay(10f));
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MenuScene");
    }
    
    void Start()
    {
        GenerateTunnels();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished && minecart.forwardSpeed > 0f && minecart.sideSpeed > 0f)
        {
            minecart.forwardSpeed -= 0.05f;
            minecart.sideSpeed -= 0.05f;

            if (minecart.forwardSpeed < 0f)
                minecart.forwardSpeed = 0f;

            if (minecart.sideSpeed < 0f)
                minecart.sideSpeed = 0f;
        }
    }
}
