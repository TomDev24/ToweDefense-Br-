using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject completeLevelUI;
    [SerializeField]
    private GameObject gameOverUI;

    public static bool gameEnded { get; private set; } = false; // again static variables are carry on from one scene to another (even after restart)
                                                                     // |
    private void Start() // called every time we load the scene;      <-
    {
        gameEnded = false;
    }

    void Update()
    {
        if (gameEnded)
            return;

        //if (Input.GetKeyDown("e"))
        //    EndGame();

        if(PlayerStats.Lives <= 0)
            EndGame();
    }

    private void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }

    public void LevelWon()
    {
        int level = PlayerPrefs.GetInt("levelReached", 1);
        PlayerPrefs.SetInt("levelReached", level + 1);

        gameEnded = true;
        completeLevelUI.SetActive(true);
    }
}
