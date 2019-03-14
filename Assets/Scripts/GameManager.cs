using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum stampTypes
{
    SimpleStamp,
    DoubleStamp,
    TripleStamp,
    ForbiddenStamp
}
public enum documentsTypes
{
    OneStampZone,
    TwoStampZone,
    ThreeStampZone
}



public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // ====== VARIABLES =======//
    public Canvas canvas;
    public RectTransform canvasRectTransform;

    public float screenRatio;
    public Vector2 resizingValues;
    public bool gameStarted;
    public GameObject DocumentPrefab;
    public List<GameObject> DocumentsList = new List<GameObject>();
    public Sprite[] StampApparences;
    public Sprite[] DocumentsApparence;
    public GameObject NextDocument;
    public int actualDocumentIndex;
    public int scorePerDoc = 100;

    public Vector2 valuesOfDocTypeEnum = new Vector2(1, 3);     // Encadre les elements dans lesquels on peut piocher les documents (permet une progression)
    public Vector2 valuesOfStampTypeEnum = new Vector2(1, 4);   // Encadre les elements dans lesquels on peut piocher les types de tampons (permet une progression)

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



        QualitySettings.vSyncCount = 0;
        canvasRectTransform = canvas.GetComponent<RectTransform>();

        float width = Screen.width;
        float height = Screen.height;
        screenRatio = width / height;

        float resizingScreenX = width / canvasRectTransform.rect.width;      
        float resizingScreenY = height / canvasRectTransform.rect.height;

        resizingValues = new Vector2(resizingScreenX, resizingScreenY);

        Debug.Log("Resizing = " + resizingScreenX + ", " + resizingScreenY );



    }

    private void Start()
    {
        DocumentsList.Add(Instantiate(DocumentPrefab, canvas.transform));
        NextDocument = (Instantiate(DocumentPrefab, canvas.transform));
        NextDocument.transform.position = NextDocument.transform.position - Vector3.right * 6;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DocumentsList.Add(Instantiate(DocumentPrefab, canvas.transform));
        }
    }
    // ====== CLASS METHODS =======//

    /// <summary>
    /// Validates the actual doc and reinitialize the data
    /// </summary>
    public void ValidStamping()
    {
        print("valid Stamping");

        ScoreManager.instance.OnDocumentComplete(scorePerDoc);


        StartCoroutine(TransitionToNewDocument());

    }
    public void InvalidStamping()
    {

    }


    public IEnumerator TransitionToNewDocument()
    {
        Vector3 startPosition = DocumentsList[actualDocumentIndex].transform.position;
        Vector3 endPosition = startPosition + Vector3.right*6;

        Vector3 startPosition2 = NextDocument.transform.position;
        Vector3 endPosition2 = startPosition2 + Vector3.right *6;

        float t = 0;
        while (t < 1)
        {
            DocumentsList[actualDocumentIndex].transform.position = Vector3.Lerp(startPosition, endPosition, t);
            NextDocument.transform.position = Vector3.Lerp(startPosition2, endPosition2, t);
            t += Time.deltaTime;
            yield return null;
        }
        DocumentsList[actualDocumentIndex].transform.position = endPosition;
        NextDocument.transform.position = endPosition2;


        DocumentsList.Add(NextDocument);
        actualDocumentIndex++;

        NextDocument = (Instantiate(DocumentPrefab, canvas.transform));
        NextDocument.transform.position = NextDocument.transform.position - Vector3.right * 6;
    }


    public void ReturnToMenu()
    {

    }

    public void GameEnded()
    {

    }
}
