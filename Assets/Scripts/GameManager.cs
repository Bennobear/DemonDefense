using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;
    public static bool gameIsOver;
    public GameObject completeLevelUI;
    public GameObject pauseUI;

    public SceneFader fader;


    // Start is called before the first frame update
    void Start()
    {
        gameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
            return;
        if (PlayerStats.life <= 0)
        {
            EndGame();
        }
        
    }

    public void EndGame()
    {
        gameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        gameIsOver = true;
        completeLevelUI.SetActive(true);
    }

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
