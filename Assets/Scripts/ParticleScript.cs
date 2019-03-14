using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{

    Canvas canvas;
    float pixelsToFall;
    float initialHeight;
    float rotationSpeed;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        pixelsToFall = canvas.GetComponent<RectTransform>().rect.height + 2.5f * gameObject.GetComponent<RectTransform>().rect.height;
        initialHeight = gameObject.GetComponent<RectTransform>().localPosition.y;
        rotationSpeed = Random.Range(-3f, 3f);
    }

    
    void Update()
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(gameObject.GetComponent<RectTransform>().localPosition.x, gameObject.GetComponent<RectTransform>().localPosition.y - 8f, gameObject.GetComponent<RectTransform>().localPosition.z);
        gameObject.GetComponent<RectTransform>().Rotate(0, 0, rotationSpeed);
        if (gameObject.GetComponent<RectTransform>().localPosition.y <= initialHeight - pixelsToFall)
        {
            Destroy(gameObject);
        }
    }
}
