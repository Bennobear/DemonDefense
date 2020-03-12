using UnityEngine;
using UnityEngine.UI;
//Scene fade and level unlocks

public class LevelSelector : MonoBehaviour
{
    public SceneFader fader;
    public Button[] levelButtons;
    //Check which is the highest level unlocked by the player and disable the other buttons accordingly
    public void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }
    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
    }
}
