using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// functions that runs when button is clicked
//updates the player answer depending on which button is clicked
public class ButtonHelpers : MonoBehaviour
{
    public AudioSource buttonClickedSound;
    public AudioClip clip;

    private int b1 = 1;
    private int b2 = 2;
    private int b3 = 3;

    [SerializeField] private AnswerTime AnswerTimeScript;

    public void AnswerChosen1()
    {
        PlayOnce();
        AnswerTimeScript.playerAnswer = b1;
    }

    public void AnswerChosen2()
    {
        PlayOnce();
        AnswerTimeScript.playerAnswer = b2;
    }

    public void AnswerChosen3()
    {
        PlayOnce();
        AnswerTimeScript.playerAnswer = b3;
    }

    //plays auido of a button clicking
    public void PlayOnce()
    {
        buttonClickedSound.PlayOneShot(clip);
    }

}
