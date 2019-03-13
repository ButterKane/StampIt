using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StampingZoneManager : MonoBehaviour
{
    public static StampingZoneManager instance;

    public Button StampingZone;
    public Button StampingZoneInstance;
    // ====== VARIABLES =======//



    // ====== MONOBEHAVIOUR METHODS =======//

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // ====== CLASS METHODS =======//
    public Vector3 GetButtonPosition()
    { 
        return Vector3.zero;
    }

    public void GenerateNewStampingZone(Vector3 newPosition, GameObject parent)
    {

        StampingZoneInstance = Instantiate(StampingZone, parent.transform); // on fait appraitre la zone de tamponnage

        StampingZoneInstance.transform.position = newPosition;    //On déplace la zone sur une nouvelle position

        StampingZoneInstance.GetComponent<StampingZoneBehaviour>().nbOfStampsNeeded = SetNumberOfStampsNeeded((stampTypes) Random.Range(GameManager.instance.valuesOfStampTypeEnum.x - 1, GameManager.instance.valuesOfStampTypeEnum.y - 1));   // ici on limite à 3 pour ne pas introduire le forbidden

        if (StampingZoneInstance.GetComponent<StampingZoneBehaviour>().nbOfStampsNeeded > 0) // si la zone instantiée n'est pas interdite
        {
            parent.GetComponent<PaperData>().stampsToValidate++;
        }
    }



    public int SetNumberOfStampsNeeded(stampTypes stampType)
    {
        switch ((int)stampType)
        {
            case 0:
                print("stamp Type is singleStamp");
                return 1;
            case 1:
                print("stamp Type is doubleStamp");
                return 2;
            case 2:
                print("stamp Type is tripleStamp");
                return 3;

            default:
                print("incorrect stamp, please check (or forbidden stamp)");
                return 0;
        }
        
    }
}
