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

    void Awake()
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

    public void GenerateNewStampingZone(Vector3 newPosition, PaperData parent, int index)
    {

        StampingZoneInstance = Instantiate(StampingZone, parent.transform); // on fait appraitre la zone de tamponnage

        StampingZoneBehaviour instanceScript = StampingZoneInstance.GetComponent<StampingZoneBehaviour>();

        StampingZoneInstance.transform.position = newPosition;    //On déplace la zone sur une nouvelle position

        instanceScript.StampData.linked_document_class = parent.documentData;
        Debug.Log(instanceScript.StampData.linked_document_class.numberOfStampsToInstantiate);

        instanceScript.StampData.nbOfStampsNeeded = parent.documentData.stampZonesToGenerate[index].nbOfStampsNeeded;   // ici on limite à 3 pour ne pas introduire le forbidden

        instanceScript.StampData.SetApparence();
        instanceScript.SetZoneApparence();

    }

}
