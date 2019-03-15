using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsBehaviours : MonoBehaviour
{

    public GameObject levelScreenInstance;
    public static GameObject lastScreen;

    public void OnStartButtonClick()
    {
        GameManager.instance.StartGame();
        gameObject.SetActive(false);
    }

    public void OnLevelButtonClick()
    {
        gameObject.SetActive(false);
        lastScreen = gameObject;
        levelScreenInstance.SetActive(true);
    }

    public void OnLevelClick()
    {
        gameObject.GetComponent<IconFadeManager>().LoadLevel();
        if (lastScreen.GetComponent<SuccessScreenManager>() != null)
        {
            gameObject.GetComponent<SuccessScreenManager>().ResetSuccessScreen();
        }
    }

    public void OnNextLevelClick()
    {
        GameManager.instance.Cleargame();
        LevelsManager.instance.MoveToNextLevel();
        GameManager.instance.StartLevel(LevelsManager.instance.actual_level);
    }

    public void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        lastScreen.SetActive(true);
    }

}
