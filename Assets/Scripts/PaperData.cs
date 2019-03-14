using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Linq;

public class PaperData : MonoBehaviour
{
    public bool documentDone;
    private RectTransform rect; 
    public DocumentClass documentData;

    public Vector3 BoundHLLocation;
    public Vector3 BoundHRLocation;
    public Vector3 BoundBLLocation;

    void Awake()
    {
        documentData.stampingZonesLocations = new List<Vector3>();
        //gameObject.transform.localScale = gameObject.transform.localScale / GameManager.instance.resizingValues;
        documentData.stampZonesValidated = 0;
        rect = gameObject.GetComponent<RectTransform>();
        this.gameObject.GetComponent<Image>().sprite = documentData.DocumentApparence;

        InitializeBounds();

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

    public void OnClickDocument()
    { 
        ScoreManager.instance.fever_amount = 0;
        ScoreManager.instance.StopFeverFeedback();
        ScoreManager.instance.score -= GameManager.instance.scorePerDoc;
    }

    public void GenerateNewDocument()
    {

        for (int i = 0; i < documentData.numberOfStampsToInstantiate; i++)
        {
            Vector3 newPosition = SetNewLocations();
            //StartCoroutine(SetNewLocations());
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
        float tempNewX = Random.Range(BoundHLLocation.x, BoundHRLocation.x);
        float tempNewY = Random.Range(BoundBLLocation.y, BoundHLLocation.y);

        //float tempNewX = Random.Range((rect.rect.width / 1080) * 200f * GameManager.instance.screenRatio, (((float)Screen.width - ((float)Screen.width / 1080) * 100f)) * GameManager.instance.screenRatio);
        //float tempNewY = Random.Range(((float)Screen.height / 1920) * 200f * GameManager.instance.screenRatio, ((float)Screen.height - (((float)Screen.height / 3f) * 1f)) * GameManager.instance.screenRatio);

        ////Vector3 newCoordinates = new Vector3(tempNewX * GameManager.instance.canvasRectTransform.localScale.x / GameManager.instance.resizingValues.x, 
        //                                    tempNewY * GameManager.instance.canvasRectTransform.localScale.y / GameManager.instance.resizingValues.y, 
        //                                    100f);

        //Vector3 newCoordinates = new Vector3(tempNewX * GameManager.instance.canvasRectTransform.localScale.x, tempNewY * GameManager.instance.canvasRectTransform.localScale.y, 100f);

        Vector3 newCoordinates = new Vector3(tempNewX, tempNewY, 100f);
        return newCoordinates;
    }



    public bool CheckIfOverlapping(Vector3 newCoordinates)
    {
        int nbOfValidations = 0;

        for (int i = 0; i < documentData.stampingZonesLocations.Count; i++)
        {
            if (newCoordinates.x <= documentData.stampingZonesLocations[i].x + 1.2f && newCoordinates.x >= documentData.stampingZonesLocations[i].x - 1.2f)               // Les 1 sont des valeurs en dur, pratiques
            {
                if (newCoordinates.y <= documentData.stampingZonesLocations[i].y + 1.2f && newCoordinates.y >= documentData.stampingZonesLocations[i].y - 1.2f)
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

    private void InitializeBounds()
    {
        GameObject firstChild = gameObject.transform.GetChild(0).gameObject;

        firstChild.transform.localScale = new Vector3(GameManager.instance.screenRatio, GameManager.instance.screenRatio, GameManager.instance.screenRatio);

        GameObject BoundHL = firstChild.transform.GetChild(0).gameObject;
        GameObject BoundHR = firstChild.transform.GetChild(1).gameObject;
        GameObject BoundBL = firstChild.transform.GetChild(2).gameObject;

        BoundHLLocation = BoundHL.GetComponent<RectTransform>().transform.position;
        BoundHRLocation = BoundHR.GetComponent<RectTransform>().transform.position;
        BoundBLLocation = BoundBL.GetComponent<RectTransform>().transform.position;

    }


    //public IEnumerator SetNewLocations()
    //{
    //    Vector3 newCoordinates = CreateNewCoordinates();

    //    Vector3 newPosition = Vector3.zero;

    //    if (documentData.stampingZonesLocations.Count > 0)
    //    {
    //        while (CheckIfOverlapping(newCoordinates))
    //        {
    //            newCoordinates = CreateNewCoordinates();
    //            print("rerolling the coordinates");
    //            yield return null;
    //        }
    //        newPosition = new Vector3(newCoordinates.x, newCoordinates.y, 100);
    //    }
    //    else
    //    {
    //        newPosition = new Vector3(newCoordinates.x, newCoordinates.y, 100);
    //    }
    //}

}
