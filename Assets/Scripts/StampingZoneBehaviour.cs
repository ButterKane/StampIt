using System.Collections;
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
        if (StampData.nbOfStampsNeeded == 0)
        {
            GameManager.instance.InvalidStamping();// fail
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
