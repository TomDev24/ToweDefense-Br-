using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour
{
    [SerializeField]
    private Text wavesText;

    private void OnEnable()
    {
        wavesText.text = PlayerStats.Rounds.ToString();
        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        wavesText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(.7f);

        while (round < PlayerStats.Rounds)
        {
            wavesText.text = round.ToString();

            round++;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
