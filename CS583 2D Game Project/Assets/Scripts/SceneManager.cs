using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneOutline : MonoBehaviour
{
    public AudioSource pencilSound;
    public AudioClip clip;

    [SerializeField] private Animator revealAnim;

    void Awake()
    {
        revealAnim.SetBool("Signed", false);
    }

    //loads the next scene
    public void LoadScene()
    {
        PlayOnce();
        revealAnim.SetBool("Signed", true);
        StartCoroutine(LoadSceneAfterDelay(5f));
    }

    //quits unity
    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameScene");
    }

    public void PlayOnce()
    {
        pencilSound.PlayOneShot(clip);
    }

}
