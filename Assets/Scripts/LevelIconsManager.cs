using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIconsManager : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        //if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            gameObject.GetComponent<RectTransform>().localPosition += new Vector3(0, touch.deltaPosition.y, 0);
        }
    }
}
