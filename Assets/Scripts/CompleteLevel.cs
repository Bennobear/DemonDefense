using UnityEngine;
//Scene Fade

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
        if(PlayerPrefs.GetInt("levelReached") >= nextLevelIndex)
        {
            Debug.Log("Level Schon geschafft!");
        }
        else
        {
            PlayerPrefs.SetInt("levelReached", nextLevelIndex);
        }
    }
}
