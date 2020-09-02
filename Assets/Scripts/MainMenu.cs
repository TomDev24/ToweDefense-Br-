using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string levelToLoad = "MainLevel";

    [SerializeField]
    private SceneFader sceneFader;

    public void Play()
    {
        sceneFader.FadeTo(levelToLoad); //FindObjectOfType requires some computation
    }

    public void Quit()
    {
        Debug.Log("Qutting"); // so we see that it works
        Application.Quit();
    }
}
