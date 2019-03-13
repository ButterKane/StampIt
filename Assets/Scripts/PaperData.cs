using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class PaperData : MonoBehaviour
{
    int numberOfStampsToInstantiate;
    public Vector3[] stampingZonesLocations;
    public int stampsToValidate;
    public int stampsValidated;
    public bool documentDone;

    void Awake()
    {
        gameObject.transform.localScale = gameObject.transform.localScale / GameManager.instance.resizingValues;
        GenerateNewDocument();
        stampsValidated = 0;
    }

    private void Update()
    {
        if (stampsValidated == stampsToValidate && !documentDone)
        {
            GameManager.instance.ValidStamping();
            documentDone = true;
        }
    }


    public void GenerateNewDocument()
    {
        Debug.Log("bidouille");
        numberOfStampsToInstantiate = 0;

        SetNumberOfStampsNeeded((documentsTypes)Random.Range(GameManager.instance.valuesOfDocTypeEnum.x-1, GameManager.instance.valuesOfDocTypeEnum.y - 1)); //between 0 and 2

        print("nbOfStamps = " + numberOfStampsToInstantiate);
        stampingZonesLocations = new Vector3[numberOfStampsToInstantiate];

        for (int i = 0; i < numberOfStampsToInstantiate; i++)
        {
            stampingZonesLocations[i] = SetNewStampingZoneLocation();
            StampingZoneManager.instance.GenerateNewStampingZone(stampingZonesLocations[i], gameObject); // on génère les zone de tamponnages à chaque nouvelle position, enfants du document
        }

    }

    public void SetNumberOfStampsNeeded(documentsTypes docType)
    {
        switch ((int)docType)
        {
            case 0:
                print("doc Type is OneStamp");
                numberOfStampsToInstantiate = 1;
                break;
            case 1:
                print("doc Type is TwoStamps");
                numberOfStampsToInstantiate = 2;
                break;
            case 2:
                print("doc Type is ThreeStamps");
                numberOfStampsToInstantiate = 3;
                break;

            default:
                print("incorrect document, please check");
                break;
        }

    }

    public Vector3 SetNewStampingZoneLocation()
    {
        //Debug.Log("screen size = " + Screen.width + ", " + Screen.height + " ; and modified : " + (Screen.width *GameManager.instance.screenRatio) + ", " + (Screen.height* GameManager.instance.screenRatio));

        Vector3 newCoordinates = CreateNewCoordinates();

        Vector3 newPosition = Vector3.zero;

        if (stampingZonesLocations[0] != null)
        {
            while (CheckIfOverlapping(newCoordinates))
            {
                newCoordinates = CreateNewCoordinates();
                print("rerolling the coordinates");
            }
            newPosition = new Vector3(newCoordinates.x , newCoordinates.y, 100);

        }
        else
        {
            newPosition = new Vector3(newCoordinates.x, newCoordinates.y, 100);
        }

        return newPosition;
    }

    public Vector3 CreateNewCoordinates()
    {
        float tempNewX = (Random.Range(200 * GameManager.instance.screenRatio, Screen.width - 200 * GameManager.instance.screenRatio));
        float tempNewY = (Random.Range(200 * GameManager.instance.screenRatio, Screen.height - ((Screen.height / 3) * 2) * GameManager.instance.screenRatio));

        Vector3 newCoordinates = new Vector3(tempNewX * GameManager.instance.canvasRectTransform.localScale.x / GameManager.instance.resizingValues.x, 
                                            tempNewY * GameManager.instance.canvasRectTransform.localScale.y / GameManager.instance.resizingValues.y, 
                                            100);

        return newCoordinates;
    }



    public bool CheckIfOverlapping(Vector3 newCoordinates)
    {
        int nbOfValidations = 0;

        foreach (Vector3 location in stampingZonesLocations)
        {
            if (newCoordinates.x <= location.x + 1 && newCoordinates.x >= location.x - 1)               // Les 1 sont des valeurs en dur, pratiques
            {
                break;
            }
            else if (newCoordinates.y <= location.x + 1 && newCoordinates.y >= location.x - 1)
            {
                break;
            }
            else
            {
                nbOfValidations++;
            }
        }

        if (nbOfValidations == stampingZonesLocations.Length)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
