using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script runs functionality of the answer portion of the game
public class AnswerTime : MonoBehaviour
{

    public Text clockComponent;
    private float countdownValue;   
    private float currCountdownValue;

    public GameObject buttonGroup;
    public Text button1;
    public Text button2;
    public Text button3;

    public Text questionText;

    public int playerAnswer;
    public static event Action OnButtonPressed;

    [SerializeField] private int roundNum;
    public int numCorrect;

    public List<string> questionBank;
    public List<int> correctAnswers;
    private List<string> button1PossAns;
    private List<string> button2PossAns;
    private List<string> button3PossAns;

    //initializes variables on awake
    void Awake()
    {
        questionBank = new List<string>();
        correctAnswers = new List<int>();
        button1PossAns = new List<string>();
        button2PossAns = new List<string>();
        button3PossAns = new List<string>();
        countdownValue = 10;
        buttonGroup.SetActive(false);
        questionText.enabled = false;
        roundNum = 0;
        numCorrect = 0;
        AddAnswersToButtons();
        AddQuestionToBank();
        AddAnswersToBank();
    }

    // initializes variable on start
    // void Start()
    // {
    //     //AnswerRound();
    //     roundNum = 0;
    // }

    // begins countdown timer
    public IEnumerator StartCountdown(float countdownValueVar)
    {
        currCountdownValue = countdownValueVar;
        while (currCountdownValue > -1)
        {
            //Debug.Log("Countdown: " + currCountdownValue);
            clockComponent.text = currCountdownValue.ToString();
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        questionText.enabled = false;
        buttonGroup.SetActive(false);
        OnButtonPressed?.Invoke();
    }

    // function called by game manager to start the answer period
    public void AnswerRound()
    {
        buttonGroup.SetActive(true);
        StartCoroutine(EnableObjectAfterDelay(1f));
        SetButtonAnswers(roundNum);
        questionText.text = questionBank[roundNum];
        playerAnswer = -1;
        StartCoroutine(StartCountdown(countdownValue));
        roundNum++;
    }


    // if button is clicked, stop countdown and add to num of correct question variable
    // then flag for game manager to move to next gamestate
    void Update()
    {
        if(playerAnswer != -1) //answer was chosen
        {
            if(playerAnswer == correctAnswers[roundNum-1])
            {
                numCorrect++;
                Debug.Log("num correct: " + numCorrect);
            }
            questionText.enabled = false;
            StopAllCoroutines();
            buttonGroup.SetActive(false);
            OnButtonPressed?.Invoke();
        }
    }

    //makes question text box visible after delay to account for monitor animation
    IEnumerator EnableObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        questionText.enabled = true;
    }

    // adds question and answers to respective lists
    private void AddQuestionToBank()
    {
        string q1 = "Q1: How many circles are there?";          //A: 4
        string q2 = "Q2: How many triangles are there?";        //A: 3
        string q3 = "Q3: How many red circles are there?";     //A: 2
        string q4 = "Q4: Which shape was there the least of?";  //A: Triangles
        string q5 = "Q5: What is the total number of circles and triangles combined?";  //A: 5

        questionBank.Add(q1);
        questionBank.Add(q2);
        questionBank.Add(q3);
        questionBank.Add(q4);
        questionBank.Add(q5);
    }

    private void AddAnswersToBank()
    {
        correctAnswers.Add(1);
        correctAnswers.Add(3);
        correctAnswers.Add(2);
        correctAnswers.Add(3);
        correctAnswers.Add(1);
    }

    private void AddAnswersToButtons()
    {
        button1PossAns.Add("4");    //q1-correct
        button1PossAns.Add("4");
        button1PossAns.Add("1");
        button1PossAns.Add("Squares");
        button1PossAns.Add("5");    //q5-correct

        button2PossAns.Add("3");
        button2PossAns.Add("2");
        button2PossAns.Add("2");    //q3-correct
        button2PossAns.Add("Circles");
        button2PossAns.Add("6");

        button3PossAns.Add("5");
        button3PossAns.Add("3");    //q2-correct
        button3PossAns.Add("5");
        button3PossAns.Add("Triangles");    //q4-correct
        button3PossAns.Add("4");
    }

    private void SetButtonAnswers(int questionNumber)
    {
        button1.text = button1PossAns[questionNumber];
        button2.text = button2PossAns[questionNumber];
        button3.text = button3PossAns[questionNumber];
    }
}
