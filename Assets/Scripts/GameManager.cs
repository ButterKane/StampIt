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
    public bool gameStarted;
    public GameObject StartButton;

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
        Debug.Log(Screen.width + ", " + Screen.height + ", " + screenRatio);

    }

    // ====== CLASS METHODS =======//

    /// <summary>
    /// Validates the actual doc and reinitialize the data
    /// </summary>
    public void ValidStamping()
    {
        ScoreManager.instance.IncreaseScore(1);
        // ici "transférer" les images sur le document tamponné (ça fera un effet de ouf)

        foreach(Image stamp in StampingManager.instance.stampList) // on "supprime" les tampons posés sur le document jusque-là 
        {
            Destroy(stamp);
        }
        StampingManager.instance.stampList.Clear();

        print("valid Stamping");
    }



    private void GetToNewLocation()
    {
        
    }

    public void ReturnToMenu()
    {
    }

    public void GameEnded()
    {

    }
}
