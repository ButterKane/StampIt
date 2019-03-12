using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;


    // ====== VARIABLES =======//
    public int score;

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
    }


    // ====== CLASS METHODS =======//

    public void IncreaseScore(int addedPoints)
    {
        score += addedPoints;
    }
}
