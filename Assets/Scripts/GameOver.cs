using UnityEngine;
using UnityEngine.SceneManagement;
//Scene Fade

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
