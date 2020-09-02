using UnityEngine.UI;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    // this is not a best implementation, link to player stats
    private Text livesText;

    private void Start()
    {
        livesText = GetComponent<Text>();
    }

    void Update()
    {
        livesText.text = PlayerStats.Lives.ToString() + " LIVES";
    }
}
