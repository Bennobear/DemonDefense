using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
//GameManager handles GameStates

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public static bool gameIsOver;
    public GameObject completeLevelUI;
    public GameObject pauseUI;
    public SceneFader fader;

    void Start()
    {
        gameIsOver = false;
    }
    //Lose game when life reaches <=0
    void Update()
    {
        if (gameIsOver)
            return;
        if (PlayerStats.life <= 0)
        {
            EndGame();
        }
    }
    //Set game is over to prevent some methods to repeat / activate lose screen
    public void EndGame()
    {
        gameIsOver = true;
        gameOverUI.SetActive(true);
    }
    //Set game is over to prevent some methods to repeat / activate win screen
    public void WinLevel()
    {
        gameIsOver = true;
        completeLevelUI.SetActive(true);
    }
    //Speedup for a set amount of time (IMPLEMENT TOGGLE HERE)
    public void Speedup()
    {
        Time.timeScale = 5f;
        StartCoroutine(Wait());
    }
    public void PauseGame()
    {
        Toggle();
    }
    public void RetryGame()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Toggle();
        fader.FadeTo("MainMenu");
    }
    //Pause menu switch state
    public void Toggle()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);
        if (pauseUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(15f);
        Time.timeScale = 1f;
    }
}
