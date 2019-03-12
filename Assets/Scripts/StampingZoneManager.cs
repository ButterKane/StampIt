using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StampingZoneManager : MonoBehaviour
{
    public static StampingZoneManager instance;

    public Button StampingZone;
    public Button StampingZoneInstance;
    public int numberOfStampsNeeded;
    public int numberOfStampsDone;
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

    public void GenerateNewStampingZone()
    {
        if (StampingZoneInstance != null)
        {
            Destroy(StampingZoneInstance);
        }

        StampingZoneInstance = Instantiate(StampingZone, GameManager.instance.canvas.transform); // on fait appraitre la zone de tamponnage

        StampingZoneInstance.onClick.AddListener(GameManager.instance.OnStampingZoneClicked);   // On référence l'event qu'on veut sur la nouvelle instance

        StampingZoneInstance.transform.position = GetNewLocation();    //On déplace la zone sur une nouvelle position

        SetNumberOfStampsNeeded((stampTypes) Random.Range(0, 3));   // le Random se fait entre 0 et le nombre de membres de l'enum
    }


    public Vector3 GetNewLocation()
    {
        //Debug.Log("screen size = " + Screen.width + ", " + Screen.height + " ; and modified : " + (Screen.width *GameManager.instance.screenRatio) + ", " + (Screen.height* GameManager.instance.screenRatio));

        float tempNewX = (Random.Range(200 * GameManager.instance.screenRatio, Screen.width - 200 * GameManager.instance.screenRatio));
        float tempNewY = (Random.Range(200 * GameManager.instance.screenRatio, Screen.height - ((Screen.height/3)*2) * GameManager.instance.screenRatio));

        //Debug.Log(tempNewX + ", " + tempNewY);

        Vector3 newPosition = new Vector3(tempNewX  * GameManager.instance.canvasRectTransform.localScale.x, tempNewY * GameManager.instance.canvasRectTransform.localScale.y, 100);

        //Debug.Log("new position =" + newPosition);

        return newPosition;
    }


    public void SetNumberOfStampsNeeded(stampTypes stampType)
    {
        switch ((int)stampType)
        {
            case 0:
                print("stamp Type is singleStamp");
                numberOfStampsNeeded = 1;
                break;
            case 1:
                print("stamp Type is doubleStamp");
                numberOfStampsNeeded = 2;
                break;
            case 2:
                print("stamp Type is tripleStamp");
                numberOfStampsNeeded = 3;
                break;

            default:
                print("incorrect stamp, please check");
                break;
        }
        
    }
}
