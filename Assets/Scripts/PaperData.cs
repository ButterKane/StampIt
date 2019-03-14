using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Linq;

public class PaperData : MonoBehaviour
{
    public bool documentDone;

    public DocumentClass documentData;

    void Awake()
    {
        documentData.stampingZonesLocations = new List<Vector3>();
        gameObject.transform.localScale = gameObject.transform.localScale / GameManager.instance.resizingValues;
        documentData.stampZonesValidated = 0;

        this.gameObject.GetComponent<Image>().sprite = documentData.DocumentApparence;

        GenerateNewDocument();
    }

    private void Update()
    {
        if (documentData.stampZonesValidated == documentData.stampZonesToValidate && !documentDone)
        {
            GameManager.instance.ValidStamping();
            documentDone = true;
        }
    }


    public void GenerateNewDocument()
    {

        for (int i = 0; i < documentData.numberOfStampsToInstantiate; i++)
        {
            Vector3 newPosition = SetNewLocations();
            documentData.stampingZonesLocations.Add(newPosition);


            StampingZoneManager.instance.GenerateNewStampingZone(newPosition, this, i); // on génère les zone de tamponnages à chaque nouvelle position, enfants du document
        }

    }

    public Vector3 SetNewLocations()
    {
        Vector3 newCoordinates = CreateNewCoordinates();

        Vector3 newPosition = Vector3.zero;

        if (documentData.stampingZonesLocations.Count > 0)
        {
            while (CheckIfOverlapping(newCoordinates))
            {
                newCoordinates = CreateNewCoordinates();
                print("rerolling the coordinates");
            }
            newPosition = new Vector3(newCoordinates.x, newCoordinates.y, 100);
        }
        else
        {
            newPosition = new Vector3(newCoordinates.x, newCoordinates.y, 100);
        }
        return newPosition;
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

        for (int i = 0; i < documentData.stampingZonesLocations.Count; i++)
        {
            if (newCoordinates.x <= documentData.stampingZonesLocations[i].x + 1f && newCoordinates.x >= documentData.stampingZonesLocations[i].x - 1f)               // Les 1 sont des valeurs en dur, pratiques
            {
                if (newCoordinates.y <= documentData.stampingZonesLocations[i].y + 1f && newCoordinates.y >= documentData.stampingZonesLocations[i].y - 1f)
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

        if (nbOfValidations == documentData.stampingZonesLocations.Count)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
