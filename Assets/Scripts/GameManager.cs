using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneFader sceneFader;
    public int nextLevelIndex = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinLevel()
    {
        Debug.Log("LEVEL WON");
        PlayerPrefs.SetInt("levelReached", nextLevelIndex);
        sceneFader.FadeTo("LevelSelection");
    }
}
