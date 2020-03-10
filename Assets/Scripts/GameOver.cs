using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public SceneFader fader;

    public void Retry()
    {
        fader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        fader.FadeTo("MainMenu");
    }
}
