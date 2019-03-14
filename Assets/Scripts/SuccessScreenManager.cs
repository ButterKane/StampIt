using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessScreenManager : MonoBehaviour
{
    public List<GameObject> successScreenElements;
    public Image fadeRenderer;
    public Text text;
    public float fadeMaxAlpha = 120f;
    public float timeToFade = 1f;
    public float timeBeforeFade = 2.5f;

    public void OnSuccess(int Score)
    {
        for (int i = 0; i < successScreenElements.Count; i++)
        {
            successScreenElements[i].SetActive(true);
        }
        text.text = "Score : " + Score;
        StartCoroutine(SuccessFade());
    }

    IEnumerator SuccessFade()
    {
        float time = 0f;
        while (time < timeBeforeFade)
        {
            time += Time.deltaTime;
            yield return null;
        }
        while (time - timeBeforeFade < timeToFade)
        {
            text.color = new Color(1, 1, 1, ((time - timeBeforeFade) / timeToFade));
            fadeRenderer.color = new Color(0, 0, 0, ((time - timeBeforeFade) / timeToFade) * (fadeMaxAlpha / 255f));
            time += Time.deltaTime;
            yield return null;
        }
        text.color = new Color(1, 1, 1, 1);
        fadeRenderer.color = new Color(0, 0, 0, fadeMaxAlpha / 255f);
    }
}
