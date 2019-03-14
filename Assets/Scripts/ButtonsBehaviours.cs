using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsBehaviours : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        GameManager.instance.StartGame();
        gameObject.SetActive(false);
    }

}
