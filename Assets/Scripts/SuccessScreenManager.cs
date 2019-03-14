using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuccessScreenManager : MonoBehaviour
{
    public static SuccessScreenManager instance;


    public List<GameObject> successScreenElements;
    public GameObject successStamp;
    public GameObject Nextlevel;
    public GameObject LevelsButton;
    public Sprite goodSprite;
    public Sprite greatSprite;
    public Sprite superSprite;
    public Sprite perfectSprite;
    public Image fadeRenderer;
    public Text text;
    public float fadeMaxAlpha = 120f;
    public float timeToFade = 1f;
    public float timeBeforeFade = 2.5f;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }



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
        successStamp.SetActive(true);
        enum_LevelRating levelRating = GameManager.instance.GetComponent<ScoreManager>().CalculateLevelRating();
        if (levelRating == enum_LevelRating.Good)
        {
            successStamp.GetComponent<Image>().sprite = goodSprite;
        }
        if (levelRating == enum_LevelRating.Great)
        {
            successStamp.GetComponent<Image>().sprite = greatSprite;
        }
        if (levelRating == enum_LevelRating.Super)
        {
            successStamp.GetComponent<Image>().sprite = superSprite;
        }
        if (levelRating == enum_LevelRating.Perfect)
        {
            successStamp.GetComponent<Image>().sprite = perfectSprite;
        }
        text.color = new Color(1, 1, 1, 1);
        fadeRenderer.color = new Color(0, 0, 0, fadeMaxAlpha / 255f);
        Nextlevel.SetActive(true);
        LevelsButton.SetActive(true);
    }
}
