using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIconsManager : MonoBehaviour
{

    public Sprite lockedLevelSprite;
    public GameObject iconPrefab;
    List<GameObject> iconsList = new List<GameObject>();

    void Start()
    {
        SpawnIcons();
    }

    
    void Update()
    {
        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            gameObject.GetComponent<RectTransform>().localPosition += new Vector3(0, touch.deltaPosition.y, 0);
        }*/
    }

    void SpawnIcons()
    {
        Sprite lastSprite = null;
        int sameLevelStreak = 1;
        foreach(var levels in LevelsManager.instance.level_data_dict)
        {
            GameObject instance = Instantiate(iconPrefab, transform);
            instance.GetComponent<RectTransform>().localPosition = new Vector3(-325f + (325f * (iconsList.Count % 3)), 450f - (220f * Mathf.Floor(iconsList.Count/3f)), 0);
            iconsList.Add(instance);
            if (levels.Value.is_unlocked)
            {
                if (lastSprite == levels.Value.level_icon)
                {
                    sameLevelStreak ++;
                }
                else
                {
                    sameLevelStreak = 1;
                }
                instance.GetComponent<IconFadeManager>().levelNumber = sameLevelStreak;
                instance.GetComponent<IconFadeManager>().levelID = levels.Key;
                instance.GetComponent<Image>().sprite = levels.Value.level_icon;
            }
            else
            {
                instance.GetComponent<Image>().sprite = lockedLevelSprite;
            }
            lastSprite = levels.Value.level_icon;
        }
    }
}
