using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampingZoneBehaviour : MonoBehaviour
{
    public int nbOfStampsNeeded;
    public int nbOfStampsDone;
    public bool isStampForbidden;

    private void Awake()
    {
        nbOfStampsDone = 0;
    }

    public void OnStamping()
    {
        if(isStampForbidden)
        {
            // fail
        }
        else
        {
            nbOfStampsDone++;
            if (nbOfStampsDone == nbOfStampsNeeded)
            {
                // validate the stampingZone in the PaperData
            }

        }
    }

}
