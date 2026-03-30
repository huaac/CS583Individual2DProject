using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// this script manages the gamestate of the scene
public enum GameState
{
    Dialogue,
    Assessment,
    AnswerPeriod,
    EndState
}


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;

    //grabs working script from the scene
    [SerializeField] private Dialogue dialogueScript;
    [SerializeField] private AssessmentRound AssessmentRoundScript;
    [SerializeField] private AnswerTime AnswerTimeScript;


    void Awake()
    {
        //creates one instance of the game manager
        Instance = this;

        // disable all scripts except for dialogue as it starts first
        dialogueScript.enabled = true;              
        AssessmentRoundScript.enabled = false;
        AnswerTimeScript.enabled = false;

        //listens into events
        Dialogue.OnDialogueFinished += HandleDialogueFinished;
        Dialogue.OnAssessmentFinished += HandleAssessmentFinished;
        AssessmentRound.FinishedViewing += HandleFinishedViewing;
        AnswerTime.OnButtonPressed += HandleButtonPressed;
    }

    void OnDestroy()
    {
        Dialogue.OnDialogueFinished -= HandleDialogueFinished;
    }

    //game begins in dialogue state
    void Start()
    {
        UpdateGameState(GameState.Dialogue);
    }

    //updates gamestate if flag triggered
    void HandleDialogueFinished()
    {
        UpdateGameState(GameState.Assessment);
    }

    //updates gamestate if flag triggered
    void HandleFinishedViewing()
    {
        UpdateGameState(GameState.AnswerPeriod);
    }

    //updates gamestate if flag triggered
    void HandleButtonPressed()
    {
        UpdateGameState(GameState.Dialogue);
    }

    void HandleAssessmentFinished()
    {
        UpdateGameState(GameState.EndState);
    }

    //switches gamestate
    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Dialogue:
                Debug.Log("Entered Dialogue");
                ExecuteDialogue();
                break;
            case GameState.Assessment:
                Debug.Log("Entered Assessment");
                ExecuteAssessment();
                break;
            case GameState.AnswerPeriod:
                Debug.Log("Entered AnswerPeriod");
                ExecuteAnswer();
                break;

            case GameState.EndState:
                Debug.Log("Entered EndState");
                EndAssessment();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }

    //function to activate next state
    void ExecuteDialogue()
    {
        dialogueScript.enabled = true;
        AssessmentRoundScript.enabled = false;
        AnswerTimeScript.enabled = false;
        dialogueScript.StartDialogue();
    }

    //function to activate next state
    void ExecuteAssessment()
    {
        dialogueScript.enabled = false;
        AssessmentRoundScript.enabled = true;
        AnswerTimeScript.enabled = false;
        AssessmentRoundScript.StartAssessment(10);
    }

    //function to activate next state
    void ExecuteAnswer()
    {
        dialogueScript.enabled = false;
        AssessmentRoundScript.enabled = false;
        AnswerTimeScript.enabled = true;
        AnswerTimeScript.AnswerRound();
    }

    //function to activate next state
    void EndAssessment()
    {
        dialogueScript.enabled = false;
        AssessmentRoundScript.enabled = false;
        AnswerTimeScript.enabled = false;
        SceneManager.LoadScene("MainMenu");
    }












    // public GameState currentState = GameState.Dialogue;

    // void Start()
    // {
    //     EnterState(currentState);
    // }

    // void EnterState(GameState state)
    // {
    //     switch (state)
    //     {
    //         case GameState.Dialogue:
    //             Debug.Log("Entered Dialogue");
    //             break;
            
    //         case GameState.Assessment:
    //             Debug.Log("Entered Assessment");
    //             break;

    //         case GameState.AnswerPeriod:
    //             Debug.Log("Entered AnswerPeriod");
    //             break;
    //     }
    // }

    // public void ChangeState(GameState newState)
    // {
    //     if (currentState == newState) return;

    //     //ExitState(currentState);
    //     currentState = newState;
    //     //EnterState(currentState);
    // }

    // void ExitState(GameState state)
    // {
    //     switch (state)
    //     {
    //         case GameState.Paused:
    //             Time.timeScale = 1f;
    //             break;
    //     }
    // }






    // public Text clockComponent;
    // private float countdownValue;
    // private float currCountdownValue;


    // [SerializeField] private Animator curtainAnim;

    // [SerializeField] private Dialogue dialogueScript;

    // public Button button1;
    // public Button button2;
    // public Button button3;

    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     //Debug.Log(dialogueScript.lines.Count);
    //     curtainAnim.SetInteger("CurtainTransition",0);
    //     countdownValue = 3;
    //     //StartCoroutine(StartCountdown(countdownValue));
    //     //dialogueScript.StartDialogue();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     //Debug.Log("textIndex: " + dialogueScript.textIndex);
    //     //Debug.Log("textSection: " + dialogueScript.lines[dialogueScript.textSection].Count);
    //     //if the dialogue section is finished, run the next assessment round
    //     //if (dialogueScript.textIndex +1 == dialogueScript.lines[dialogueScript.textSection].Count)
    //     if (dialogueScript.finishedSection == true)
    //     {
    //         MoveCurtainUp();
    //         AssessmentRound();

    //         if (currCountdownValue == 0)
    //         {
    //             currCountdownValue = countdownValue;
    //             PlayerAnswerRound();
    //         }

    //     }

    // }

    
    // public IEnumerator StartCountdown(float countdownValueVar)
    // {
    //     currCountdownValue = countdownValueVar;
    //     while (currCountdownValue > -1)
    //     {
    //         //Debug.Log("Countdown: " + currCountdownValue);
    //         clockComponent.text = currCountdownValue.ToString();
    //         yield return new WaitForSeconds(1.0f);
    //         currCountdownValue--;
    //     }
    // }

    // private void MoveCurtainUp()
    // {
    //     curtainAnim.SetInteger("CurtainTransition",1);
    // }

    // private void MoveCurtainDown()
    // {
    //     curtainAnim.SetInteger("CurtainTransition",2);
    // }

    // private void AssessmentRound()
    // {
    //     MoveCurtainUp();
    //     StartCoroutine(StartCountdown(countdownValue));
    //     //
    // }

    // private void PlayerAnswerRound()
    // {
    //     StartCoroutine(StartCountdown(5));
    //     MoveCurtainDown();

    // }


}
