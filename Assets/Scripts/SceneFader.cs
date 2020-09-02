using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    [SerializeField]
    private Image img;
    [SerializeField]
    private AnimationCurve fadeCurve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    private IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
           
            t -= Time.deltaTime; // why we using time delta time outside update? // also can add speed
            float a = fadeCurve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return null;
        }
    }

    private IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {

            t += Time.deltaTime; // why we using time delta time outside update? // also can add speed
            float a = fadeCurve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return null;
        }
        
        SceneManager.LoadScene(scene);
    }


}
