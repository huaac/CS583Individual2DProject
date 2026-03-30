using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this script runs the functionality of the assessment period
public class AssessmentRound : MonoBehaviour
{
    public Text clockComponent;
    private float countdownValue;
    private float currCountdownValue;

    [SerializeField] private Animator curtainAnim;
    public static event Action FinishedViewing;

    private int roundNum;
    public List<GameObject> roundNumberItems;

    //initializes variables upon awake
    void Awake()
    {
        curtainAnim.SetInteger("CurtainTransition",0);
        countdownValue = 10;
        SetAllRoundItemsFalse();
        roundNum = 0;
        //StartCoroutine(StartCountdown(countdownValue));
        //StartAssessment();
    }

    // sets all items in each round to be invisible
    void SetAllRoundItemsFalse()
    {
        foreach (GameObject obj in roundNumberItems)
        {
            obj.SetActive(false);
        } 
    }


    //starts time countdown
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
        roundNumberItems[roundNum].SetActive(false);
        roundNum++;
        MoveCurtainDown();
    }

    //animation to move curtain up
    private void MoveCurtainUp()
    {
        curtainAnim.SetInteger("CurtainTransition",1);
    }

    //animation to move curtain down
    private void MoveCurtainDown()
    {
        curtainAnim.SetInteger("CurtainTransition",2);
        FinishedViewing?.Invoke();
    }

    // function called in game manager to start the assessment
    public void StartAssessment(int time)
    {
        if (roundNum >= roundNumberItems.Count)
        {
            Debug.Log("No more assessment rounds.");
        return;
        }
        roundNumberItems[roundNum].SetActive(true);
        MoveCurtainUp();
        StartCoroutine(StartCountdown(time));
        //
    }



}
