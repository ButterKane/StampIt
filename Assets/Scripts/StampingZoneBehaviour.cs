using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampingZoneBehaviour : MonoBehaviour
{
    public int nbOfStampsNeeded;
    public int nbOfStampsDone;

    private void Awake()
    {
        nbOfStampsDone = 0;
    }

}
