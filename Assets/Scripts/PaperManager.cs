using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PaperManager : MonoBehaviour
{

    int numberOfStampsToInstantiate;
    public List<Vector2> stampingZonesLocations;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("bisoille");
            GenerateNewDocument();
        }
    }


    public void GenerateNewDocument()
    {

        SetNumberOfStampsNeeded((documentsTypes)Random.Range(0, 3)); //between 0 and 2
        print("nbOfStamps = " + numberOfStampsToInstantiate);

        for (int i = 0; i < numberOfStampsToInstantiate; i++)
        {
            stampingZonesLocations.Add(SetNewStampingZoneLocation());
            Debug.Log("location" + i + " = " + stampingZonesLocations[i]);
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
        Debug.Log(newCoordinates);

        Vector3 newPosition = Vector3.zero;

        if (stampingZonesLocations.Count >0)
        {
            if (!CheckIfOverlapping(newCoordinates))
            {
                newPosition = new Vector3(newCoordinates.x * GameManager.instance.canvasRectTransform.localScale.x, newCoordinates.y * GameManager.instance.canvasRectTransform.localScale.y, 100);
            }
            else
            {
                SetNewStampingZoneLocation();
            }

        }
        else
        {
            newPosition = new Vector3(newCoordinates.x * GameManager.instance.canvasRectTransform.localScale.x, newCoordinates.y * GameManager.instance.canvasRectTransform.localScale.y, 100);
        }

        //Debug.Log("new position =" + newPosition);

        return newPosition;
    }

    public Vector3 CreateNewCoordinates()
    {
        float tempNewX = (Random.Range(200 * GameManager.instance.screenRatio, Screen.width - 200 * GameManager.instance.screenRatio));
        float tempNewY = (Random.Range(200 * GameManager.instance.screenRatio, Screen.height - ((Screen.height / 3) * 2) * GameManager.instance.screenRatio));

        Vector3 newCoordinates = new Vector3(tempNewX * GameManager.instance.canvasRectTransform.localScale.x, tempNewY * GameManager.instance.canvasRectTransform.localScale.y, 100);

        return newCoordinates;
    }



    public bool CheckIfOverlapping(Vector3 newCoordinates)
    {
        int nbOfValidations = 0;

        foreach (Vector3 location in stampingZonesLocations)
        {
            if (newCoordinates.x <= location.x + 400 && newCoordinates.x >= location.x - 400)
            {
                break;
            }
            else if (newCoordinates.y <= location.x + 400 && newCoordinates.y >= location.x - 400)
            {
                break;
            }
            else
            {
                nbOfValidations++;
            }
        }

        if (nbOfValidations == stampingZonesLocations.Count)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
