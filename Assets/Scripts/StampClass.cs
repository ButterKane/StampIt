using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StampClass
{
    public int nbOfStampsNeeded;
    public int nbOfStampsDone;
    public DocumentClass linked_document_class;

    public Sprite StampZoneApparence; 

    public void SetApparence()
    {
        switch(nbOfStampsNeeded)
        {
            case 0:
                StampZoneApparence = GameManager.instance.StampZoneApparences[0];
                
                linked_document_class.stampZonesValidated++;
                break;
            case 1:
                StampZoneApparence = GameManager.instance.StampZoneApparences[1];
                break;
            case 2:
                StampZoneApparence = GameManager.instance.StampZoneApparences[2];
                break;
            case 3:
                StampZoneApparence = GameManager.instance.StampZoneApparences[3];
                break;
            default:
                Debug.Log("invalide, please check");
                break;


        }

    }
}
