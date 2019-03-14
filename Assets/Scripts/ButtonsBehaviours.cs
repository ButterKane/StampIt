using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsBehaviours : MonoBehaviour
{

    public GameObject levelScreenInstance;

    public void OnStartButtonClick()
    {
        GameManager.instance.StartGame();
        gameObject.SetActive(false);
    }

    public void OnLevelButtonClick()
    {
        gameObject.SetActive(false);
        levelScreenInstance.SetActive(true);
    }

    public void OnLevelClick()
    {
        gameObject.GetComponent<IconFadeManager>().LoadLevel();
    }

    public void OnNextLevelClick()
    {
        GameManager.instance.Cleargame();
        LevelsManager.instance.MoveToNextLevel();
        GameManager.instance.StartLevel(LevelsManager.instance.actual_level);
    }

}
