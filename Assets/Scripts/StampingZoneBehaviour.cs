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
            GameManager.instance.InvalidStamping();// fail
        }
        else
        {
            nbOfStampsDone++;
            if (nbOfStampsDone == nbOfStampsNeeded)
            {
                GetComponentInParent<PaperData>().stampsValidated++;

            }

        }
    }

}
