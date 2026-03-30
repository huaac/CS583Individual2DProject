using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

// this script manages dialogue functionality
public class Dialogue : MonoBehaviour
{
    public static event Action OnDialogueFinished;
    public static event Action OnAssessmentFinished;

    public Text textComponent;
    public List<List<string>> lines;    //contains all lines per section
    [SerializeField] private float textSpeed;

    public int textSection;
    public int textIndex;
    public bool finishedSection;

    [SerializeField] private AnswerTime AnswerTimeScript;
    public Text scoreboard;

    public AudioSource audioSource;
    public AudioClip clip;

    // initializes variables upon awake
    void Awake()
    {
        //GameManager.OnGameStateChanged += GameMangerOnGameStateChanged;
        lines = new List<List<string>>();
        AddLines();
        finishedSection = false;
        textComponent.text = string.Empty;
        textSection = 0;
        textIndex = 0;
    }


    // if player clicks on screen, dialogue auto reveals if currently in text, else goes to next line
    void Update()
    {
        Debug.Log("textSection: " + textSection);
        Debug.Log("textIndex: " + textIndex);
        if(Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[textSection][textIndex])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[textSection][textIndex];
            }
        }
        if(finishedSection == true)
        {
            textSection++;
            textIndex = 0;
        }
    }

    //function called in gamestate to start the next section of the dialogue
    public void StartDialogue()
    {
        gameObject.SetActive(true);
        finishedSection = false;
        StartCoroutine(TypeLine());
    }

    // same as start dialogue but starts after a delay
    IEnumerator StartDialogueAfterDelay()
    {
        yield return new WaitForSeconds(6f);
        gameObject.SetActive(true);
        finishedSection = false;
        StartCoroutine(TypeLine());
    }

    // reveals the lines char by char
    IEnumerator TypeLine()
    {
        
        //Type each char 1 by 1
        foreach (char c in lines[textSection][textIndex].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    //moves to the next line within the section
    void NextLine()
    {
        gameObject.SetActive(true);
        textComponent.text = string.Empty;
        if(textIndex < lines[textSection].Count-1)
        {
            textIndex++;
            StartCoroutine(TypeLine());
        }
        else //if reach the end of the dialogue segment
        {
            if(textSection > 5)
            {
                //SceneManager.LoadScene("MainMenu");
                OnAssessmentFinished?.Invoke();
            }
            if(textSection == 5)  // marks the end of the exam and begins to reveal results
            {
                //play drumroll audio
                PlayOnce();
                StartCoroutine(RevealScoreAfterDelay(2.5f));    //reveals score from exam

                if (AnswerTimeScript.numCorrect >= 3)   //plays lines if player passes exam
                {
                    textSection++;
                }
                else    //plays line if player loses exam
                {
                    textSection = textSection + 2;
                }
                textIndex = 0;
                StartCoroutine(StartDialogueAfterDelay());
            }
            else    //if end of section, raise event flag for game manager to move to next gamestate
            {
                gameObject.SetActive(false);
                finishedSection = true;
                OnDialogueFinished?.Invoke();
            }
        }
    }

    void AddLines() //adds all lines to the lines var
    {
        List<string> section1 = new List<string>
        {
            "Welcome.", //identification and analysis skills
            "My name is Maria and I will be guiding you along this assessment.",
            "This assessment was made to test individuals on their identification and analysis capabilities.",
            "There will be 5 rounds. Each round contains an identification period and an answer period.",
            "In the identification period, you will be given 10 seconds to identify the items on the screen.",
            "Afterwards, you will be given 10 seconds to answer the question on the monitor.",
            "Choose from the 3 buttons that appear on the panel in front of you to select your answer.",
            "Good Luck.",
            "The exam will begin in 3...",
            "2...",
            "1..."
        };
        List<string> section2 = new List<string>
        {
            "...",
            "Question 1 is the easiest question.",
            "Let's throw another shape into the pool."
        };
        List<string> section3 = new List<string>
        {
            "...",
            "Let's try a slightly more difficult question regarding color."
        };
        List<string> section4 = new List<string>
        {
            "Hmm...",
            "For the next question, let's change it up."
        };
        List<string> section5 = new List<string>
        {
            "...",
            "The final question will test your calculation skills."
        };

        List<string> section6 = new List<string>
        {
            "The assessment has now concluded.",
            "To pass this assessment, test takers must score at least 3 out of 5 correct.",
            "Your final score is....."
        };
        List<string> section7 = new List<string> //if passing score
        {
            "Congratulations.",
            "You have the mental capabilites we are looking for.",
            "......",
            "How would you like to join the CIA?"
        };
        List<string> section8 = new List<string> //if losing score
        {
            "Unfortunately..",
            "You did not pass the assessment.",
            "...",
            "However...",
            "If you feel like you can do better, I encourage you to retake the assessment.",
            "...I look forward to seeing you again soon.",
        };


        lines.Add(section1);
        lines.Add(section2);
        lines.Add(section3);
        lines.Add(section4);
        lines.Add(section5);
        lines.Add(section6);
        lines.Add(section7);
        lines.Add(section8);
    }


    //plays audio clip
    public void PlayOnce()
    {
        audioSource.PlayOneShot(clip);
    }

    //reveals score after a delay
    IEnumerator RevealScoreAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        scoreboard.text = AnswerTimeScript.numCorrect.ToString() + " / 5";
    }
}
