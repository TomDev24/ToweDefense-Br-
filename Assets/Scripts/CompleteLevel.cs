using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField]
    private SceneFader sceneFader;

    [SerializeField]
    private string mainMenuName = "MainMenu";

    public string nextLevel = "Level02";
    //public int levelToUnlock = 2;

    public void Continue()
    {
        //PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }
    public void Menu()
    {
        sceneFader.FadeTo(mainMenuName);
    }
}
