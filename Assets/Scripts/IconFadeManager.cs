﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconFadeManager : MonoBehaviour
{

    public GameObject child;
    public int levelNumber;
    public int levelID;
    Button button;
    Image image;
    RectTransform rectTransform;
    float minimalHeight = -500f;
    float maximalHeight = 554f;
    float fadeHeight = 100f;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        image = gameObject.GetComponent<Image>();
        rectTransform = gameObject.GetComponent<RectTransform>();
        if (levelNumber != 0)
        {
            child.GetComponent<Text>().text = levelNumber.ToString();
        }
    }

    public void OnIconsMovement()
    {
        if (rectTransform.localPosition.y < minimalHeight + fadeHeight || rectTransform.localPosition.y > maximalHeight - fadeHeight)
        {
            button.interactable = false;
            if (rectTransform.localPosition.y <= minimalHeight || rectTransform.localPosition.y >= maximalHeight)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            }
            else if (rectTransform.localPosition.y < minimalHeight + fadeHeight)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1 - (Mathf.Abs((rectTransform.localPosition.y - minimalHeight - fadeHeight) / fadeHeight)));
                print(Mathf.Abs((rectTransform.localPosition.y - minimalHeight + fadeHeight) / fadeHeight));
            }
            else if (rectTransform.localPosition.y > maximalHeight - fadeHeight)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1 - (Mathf.Abs((rectTransform.localPosition.y - maximalHeight + fadeHeight) / fadeHeight)));
                print(Mathf.Abs((rectTransform.localPosition.y - maximalHeight + fadeHeight) / fadeHeight));
            }
        }
        else
        {
            button.interactable = true;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        }
    }

    public void LoadLevel()
    {
        transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
        transform.parent.gameObject.transform.parent.gameObject.GetComponent<LevelIconsManager>().ClearIcons();
        GameManager.instance.StartLevel(levelID);
    }
}
