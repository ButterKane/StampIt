﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StampingZoneBehaviour : MonoBehaviour
{
    public StampClass StampData;

    private void Awake()
    {
        StampData.nbOfStampsDone = 0;
    }

    public void SetZoneApparence()
    {
        gameObject.GetComponent<Image>().sprite = StampData.StampZoneApparence;
    }

        public void OnStamping()
    {
        Instantiate(GameManager.instance.ValidStampFX, gameObject.transform);
        if (StampData.nbOfStampsNeeded == 0)
        {
            ScoreManager.instance.fever_amount = 0;
            ScoreManager.instance.StopFeverFeedback();
            ScoreManager.instance.score -= (GameManager.instance.scorePerDoc * 3);
        }
        else
        {
            StampData.nbOfStampsDone++;

            if (StampData.nbOfStampsDone == StampData.nbOfStampsNeeded)
            {
                GetComponentInParent<PaperData>().documentData.stampZonesValidated++;

            }

        }
    }

}
