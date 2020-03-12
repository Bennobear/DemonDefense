using UnityEngine;
//Scene Fade

public class MainMenu : MonoBehaviour
{
    public SceneFader fader;
    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }
}
