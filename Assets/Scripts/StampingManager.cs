using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StampingManager : MonoBehaviour
{
    public static StampingManager instance;

    // ====== VARIABLES =======//
    private Camera mainCamera;

    private Touch touch;
    private bool canTouch;

    public Vector2 touchingPos;
    public bool publicTouchingBool;
    private bool isTouching;
    public Image stamp;
    public List<Image> stampList;
    

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
        canTouch = true;            
        mainCamera = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gamePlaying)
        {
            publicTouchingBool = CheckIfTouching();
        }
    }


    // ====== CLASS METHODS =======//

    public bool CheckIfTouching()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);    //on récupère l'input du PREMIER doigt qui touche (index 0)
            Vector2 touchPos = touch.position;
            if (touch.phase == TouchPhase.Began && canTouch)
            {
                Image stampInstance = Instantiate(stamp, GameManager.instance.DocumentsList[GameManager.instance.actualDocumentIndex].transform);
                stampList.Add(stampInstance);
                //stampInstance.transform.position = new Vector2( touchPos.x, touchPos.y); //Marche avec Canvas en Overlay

                stampInstance.transform.position = mainCamera.ScreenToWorldPoint(new Vector3 (touchPos.x, touchPos.y, GameManager.instance.canvas.transform.position.z)); // fonctionne avec Canvas en World

                isTouching = true;
                canTouch = false;
            }
            else if (touch.phase == TouchPhase.Ended && !canTouch)
            {
                isTouching = false;
                touchPos = -Vector2.one;
                canTouch = true;
            }
        }
        return isTouching;
    }
}
