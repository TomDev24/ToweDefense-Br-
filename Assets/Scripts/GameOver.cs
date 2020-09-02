using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private SceneFader sceneFader;

    [SerializeField]
    private string mainMenuName = "MainMenu";

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //bui;d index or name of the scene but better...
        //In older versrion (Brackeys) had problem with ligthing after reload
        //builded light in light settings
    }

    public void Menu()
    {
        sceneFader.FadeTo(mainMenuName);
    }
}
