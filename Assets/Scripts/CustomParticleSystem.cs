using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomParticleSystem : MonoBehaviour
{

    public GameObject particleToSpawn;
    Canvas canvas;
    float heightToSpawn;
    float canvasWidth;
    float time;
    float timeToSpawn = 0.2f;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        heightToSpawn = canvas.GetComponent<RectTransform>().rect.height / 2f + 0.625f * particleToSpawn.GetComponent<RectTransform>().rect.height;
        canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
    }


    void Update()
    {
        time += Time.deltaTime;
        if (time >= timeToSpawn)
        {
            time -= timeToSpawn;
            GameObject particleInstance = Instantiate(particleToSpawn, transform.position, transform.rotation, transform);
            particleInstance.GetComponent<RectTransform>().localPosition = new Vector3(Random.Range(-canvasWidth / 2f, canvasWidth / 2f), heightToSpawn);
        }
    }
}
