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

public enum enum_GameState
{
    startmenu,
    levelselection,
    ingame,
    paused,
    success,
    failure
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // ====== VARIABLES =======//
    public Canvas canvas;
    public RectTransform canvasRectTransform;

    public GameObject TapFX;
    public GameObject FeverFX;
    public GameObject FastFX;
    public GameObject ValidStampFX;
    public Sprite[] StampApparences;
    public Sprite[] StampZoneApparences; // à ranger: invalid, 1 tampon, 2 tampons, 3 tampons
    public Sprite[] DocumentsApparence;


    public float screenRatio;
    public Vector2 resizingValues;
    public bool gamePlaying;
    public List<GameObject> DocumentPrefabs;
    public List<GameObject> DocumentsList = new List<GameObject>();

    List<GameObject> listOfDocsForLevel;

    private bool isLastDocument;

    public GameObject NextDocument;
    public int actualDocumentIndex;
    public int scorePerDoc = 100;

    public Vector2 valuesOfDocTypeEnum = new Vector2(1, 3);     // Encadre les elements dans lesquels on peut piocher les documents (permet une progression)
    public Vector2 valuesOfStampTypeEnum = new Vector2(1, 4);   // Encadre les elements dans lesquels on peut piocher les types de tampons (permet une progression)

    private Vector3 locationForActualDoc = new Vector3(0, -280, 0);

    public  int                 remaining_documents_amount      = 1;
    public  enum_GameState      game_state                      ;

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

    }

    private void GetDocumentToGenerate()
    {
        listOfDocsForLevel = LevelsManager.instance.level_data_dict[LevelsManager.instance.actual_level].level_document_types;
    }

    public void StartGame()
    {
        gamePlaying = true;
        StartLevel(0);

        
    }

    public void Cleargame()
    {
        foreach(GameObject doc in DocumentsList)
        {
            Destroy(doc);
        }
        DocumentsList.Clear();
        NextDocument = null;
        isLastDocument = false;
        actualDocumentIndex = 0;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    DocumentsList.Add(Instantiate(DocumentPrefabs[2], canvas.transform));
        //}
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
        if (isLastDocument)
        {
            GameEnded();
        }

    }



    public IEnumerator TransitionToNewDocument()
    {
        StampingManager.instance.canTouch = false;
        Vector3 startPosition = DocumentsList[actualDocumentIndex].transform.position;
        Vector3 endPosition = startPosition + (Vector3.right * 15f) * screenRatio;

        Vector3 startPosition2 = Vector3.zero;
        Vector3 endPosition2 = Vector3.zero;

        if (NextDocument != null)
        {
            startPosition2 = NextDocument.transform.position;
            endPosition2 = new Vector3(NextDocument.transform.position.x + (9.7f * screenRatio), NextDocument.transform.position.y + (6f * screenRatio), NextDocument.transform.position.z);
        }

        float t = 0;
        while (t < 1)
        {
            DocumentsList[actualDocumentIndex].transform.position = Vector3.Lerp(startPosition, endPosition, t);
            if (NextDocument != null)
            {
                NextDocument.transform.position = Vector3.Lerp(startPosition2, endPosition2, t);
            }
            t += Time.deltaTime;
            yield return null;
        }
        DocumentsList[actualDocumentIndex].transform.position = endPosition;

        if (NextDocument != null)
        {
            NextDocument.transform.position = endPosition2;
            DocumentsList.Add(NextDocument);
            ScoreManager.instance.StartDocumentFastTime();
            NextDocument = null;
        }

        remaining_documents_amount--;
        actualDocumentIndex++;

        if (remaining_documents_amount > 1)
        {
            NextDocument = (Instantiate(DocumentPrefabs[Random.Range(0, listOfDocsForLevel.Count - 1)], canvas.transform));
            NextDocument.transform.position = new Vector3(NextDocument.transform.position.x - (9.7f * screenRatio), NextDocument.transform.position.y - (6f * screenRatio), NextDocument.transform.position.z);
        }
        else
        {
            isLastDocument = true;
        }
        StampingManager.instance.canTouch = true;

    }

    /// <summary>
    /// Starts game with the datas from Level Manager's actual level index.
    /// </summary>
    public void StartLevel(int index)
    {
        LevelsManager.instance.ResetLevelState();
        LevelsManager.instance.actual_level = index;
        InitialiseLevel();

        game_state = enum_GameState.ingame;

        GetDocumentToGenerate();
        actualDocumentIndex = 0;

        DocumentsList.Add(Instantiate(DocumentPrefabs[Random.Range(0, listOfDocsForLevel.Count - 1)], canvas.transform));

        NextDocument = (Instantiate(DocumentPrefabs[Random.Range(0, listOfDocsForLevel.Count - 1)], canvas.transform));
        NextDocument.transform.position = new Vector3(NextDocument.transform.position.x - (9.7f * screenRatio), NextDocument.transform.position.y - (6f * screenRatio), NextDocument.transform.position.z);

        return;
    }

    /// <summary>
    /// Initialises game state with the datas from Level Manager's actual level index.
    /// </summary>
    public void InitialiseLevel()
    {
        // nbr of documents
        remaining_documents_amount = LevelsManager.instance.level_data_dict[LevelsManager.instance.actual_level].nbr_of_documents;


        return;
    }

    /// <summary>
    /// Called when the player fails a stamp.
    /// </summary>
    public void OnStampFailed()
    {

        return;
    }



    public void ReturnToMenu()
    {

    }

    public void GameEnded()
    {
        game_state = enum_GameState.success;
        SuccessScreenManager.instance.OnSuccess(ScoreManager.instance.score);
    }
}
