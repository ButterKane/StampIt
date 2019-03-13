using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Linq;

public class PaperData : MonoBehaviour
{
    int numberOfStampsToInstantiate;
    public List<Vector3> stampingZonesLocations;
    public int stampsToValidate;
    public int stampsValidated;
    public bool documentDone;

    void Awake()
    {
        stampingZonesLocations = new List<Vector3>();
        gameObject.transform.localScale = gameObject.transform.localScale / GameManager.instance.resizingValues;
        stampsValidated = 0;

        GenerateNewDocument();
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
        numberOfStampsToInstantiate = 0;

        SetNumberOfStampsNeeded((documentsTypes)Random.Range(GameManager.instance.valuesOfDocTypeEnum.x-1, GameManager.instance.valuesOfDocTypeEnum.y)); //between 0 and 2

        Debug.Log("TailleArrayPositions = " + stampingZonesLocations.Count);

        for (int i = 0; i < numberOfStampsToInstantiate; i++)
        {
            //stampingZonesLocations[i] = SetNewStampingZoneLocation();
            StartCoroutine(SetNewLocations());
            //stampingZonesLocations.Add(SetNewStampingZoneLocation());
            //StampingZoneManager.instance.GenerateNewStampingZone(stampingZonesLocations[i], gameObject); // on génère les zone de tamponnages à chaque nouvelle position, enfants du document
        }

    }

    public void SetNumberOfStampsNeeded(documentsTypes docType)
    {
        switch ((int)docType)
        {
            case 0:
                //print("doc Type is OneStamp");
                numberOfStampsToInstantiate = 1;
                gameObject.GetComponent<Image>().sprite = GameManager.instance.DocumentsApparence[0];
                break;
            case 1:
                //print("doc Type is TwoStamps");
                numberOfStampsToInstantiate = 2;
                gameObject.GetComponent<Image>().sprite = GameManager.instance.DocumentsApparence[1];
                break;
            case 2:
                //print("doc Type is ThreeStamps");
                numberOfStampsToInstantiate = 3;
                gameObject.GetComponent<Image>().sprite = GameManager.instance.DocumentsApparence[2];
                break;

            default:
                print("incorrect document, please check");
                break;
        }

    }

    public IEnumerator SetNewLocations()
    {
        Vector3 newCoordinates = CreateNewCoordinates();

        Vector3 newPosition = Vector3.zero;

        if (stampingZonesLocations.Count > 0)
        {
            while (CheckIfOverlapping(newCoordinates))
            {
                newCoordinates = CreateNewCoordinates();
                yield return null;
                print("rerolling the coordinates");
            }
            newPosition = new Vector3(newCoordinates.x, newCoordinates.y, 100);

        }
        else
        {
            newPosition = new Vector3(newCoordinates.x, newCoordinates.y, 100);
        }

        stampingZonesLocations.Add(newPosition);
        StampingZoneManager.instance.GenerateNewStampingZone(stampingZonesLocations[stampingZonesLocations.Count-1], gameObject); // on génère les zone de tamponnages à chaque nouvelle position, enfants du document
    }


    public Vector3 CreateNewCoordinates()
    {
        float tempNewX = (Random.Range(350f * GameManager.instance.screenRatio, (float)Screen.width - 400f * GameManager.instance.screenRatio));
        float tempNewY = (Random.Range(400f * GameManager.instance.screenRatio, (float)Screen.height - (((float)Screen.height / 3f) * 2f) * GameManager.instance.screenRatio));

        Vector3 newCoordinates = new Vector3(tempNewX * GameManager.instance.canvasRectTransform.localScale.x / GameManager.instance.resizingValues.x, 
                                            tempNewY * GameManager.instance.canvasRectTransform.localScale.y / GameManager.instance.resizingValues.y, 
                                            100f);
        return newCoordinates;
    }



    public bool CheckIfOverlapping(Vector3 newCoordinates)
    {
        int nbOfValidations = 0;

        for (int i = 0; i < stampingZonesLocations.Count; i++)
        {
            if (newCoordinates.x <= stampingZonesLocations[i].x + 1f && newCoordinates.x >= stampingZonesLocations[i].x - 1f)               // Les 1 sont des valeurs en dur, pratiques
            {
                if (newCoordinates.y <= stampingZonesLocations[i].y + 1f && newCoordinates.y >= stampingZonesLocations[i].y - 1f)
                {
                    break;
                }
                else nbOfValidations++;
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
