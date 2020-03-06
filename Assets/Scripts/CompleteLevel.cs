using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    public SceneFader fader;

    public string nextLevel = "Level02";
    public int nextLevelIndex = 2;

    public void Continue()
    {
        fader.FadeTo(nextLevel);
    }

    public void Menu()
    {
        fader.FadeTo("MainMenu");
    }

    private void OnEnable()
    {
        PlayerPrefs.SetInt("levelReached", nextLevelIndex);
    }
}
