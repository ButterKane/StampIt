using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        GameManager.instance.gameStarted = true;
        StampingZoneManager.instance.GenerateNewStampingZone();
        GameManager.instance.StartButton.SetActive(false);
    }
}
