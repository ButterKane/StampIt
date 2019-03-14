using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enum_LevelRating
{
    None,
    Good,
    Great,
    Super,
    Perfect
}

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;


// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Runtime")]
    public  int                     score                                   ;
    public  bool                    is_in_fast_time                         ;
    public  int                     fever_amount                            ;
    public  int                     fever_amount_limit                      = 6;
    public  int                     actual_score_multiplier                 = 1;
    public  int                     maximum_score_obtainable                ;

[Space(10)][Header("Gameplay")]
    public  float                   fast_time_duration_per_stampZone        = 0.50f;
    public  float                   rating_min_percent_great                = 0.20f;
    public  float                   rating_min_percent_super                = 0.35f;
    public  float                   rating_min_percent_perfect              = 0.70f;

// = = =

// = = = [ VARIABLES PROPERTIES ] = = =

    public int  FeverAmount
    {
        get { return fever_amount; }
        set
        {
            if (value <= fever_amount_limit)
            {
                fever_amount = value;
                ApplyFeverChange();
            }
            else
            {
                Debug.Log("Fever already at maximum value");
            }
        }
    }

// = = =

// = = = [ MONOBEHAVIOR METHODS ] = = =

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

    // DEBUG UPDATE
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) == true)
        {
            FeverAmount += 1;
        }
        if (Input.GetKeyDown(KeyCode.R) == true)
        {
            FeverAmount = 0;
        }
        if (Input.GetKeyDown(KeyCode.P) == true)
        {
            OnDocumentComplete(10);
        }
    }

// = = =

// = = = [ CLASS METHODS ] = = =

    public void IncreaseScore(int addedPoints)
    {
        score += addedPoints;
    }

    /// <summary>
    /// Increases Fever value by 1.
    /// </summary>
    public void IncreaseFever()
    {
        FeverAmount += 1;
        return;
    }

    /// <summary>
    /// Updates the fever and the score values. Called when a document is completed. 
    /// </summary>
    public void OnDocumentComplete(int document_score_gain)
    {
        UpdateMaximumScore();
        IncreaseScore( CalculateFinalScoreGained(document_score_gain) );
        if(is_in_fast_time == true) { IncreaseFever(); }
        return;
    }

    /// <summary>
    /// Updates the maximum obtainable score value, which is used to calculate end of level rating. 
    /// </summary>
    public void UpdateMaximumScore()
    {
        maximum_score_obtainable += GameManager.instance.DocumentsList[GameManager.instance.actualDocumentIndex].GetComponent<PaperData>().documentData.localScore * (1+ Mathf.Min(GameManager.instance.actualDocumentIndex, 6));
        return;
    }

    /// <summary>
    /// Applies the needed changes according to the actual "fever_amount" value. Called on every FeverAmount value change.
    /// </summary>
    public void ApplyFeverChange()
    {
        switch (fever_amount)
        {
            case 0:
                ChangeScoreMultiplier(1);
                StopFeverFeedback();
            break;

            case 1:
                ChangeScoreMultiplier(1);
            break;

            case 2:
                ChangeScoreMultiplier(2);
                StartFeverFeedback();
            break;

            case 3:
                ChangeScoreMultiplier(3);
            break;

            case 4:
                ChangeScoreMultiplier(4);
            break;

            case 5:
                ChangeScoreMultiplier(5);
            break;

            case 6:
                ChangeScoreMultiplier(6);
            break;
        }

        return;
    }

    /// <summary>
    /// Changes the actual score multiplier value.
    /// </summary>
    public void ChangeScoreMultiplier(int new_multiplier)
    {
        actual_score_multiplier = new_multiplier;
        Debug.Log("New score multiplier: <b>" + new_multiplier + "</b>");
        return;
    }

    /// <summary>
    /// Starts every feedbacks linked to the Fever time.
    /// </summary>
    public void StartFeverFeedback()
    {
        Debug.Log("Entering FEVER time");
        return;
    }

    /// <summary>
    /// Stops every feedbacks linked to the Fever time.
    /// </summary>
    public void StopFeverFeedback()
    {
        Debug.Log("Quitting FEVER time");
        return;
    }

    /// <summary>
    /// Starts the actual document FastTime coroutine, stoping the last one if it was still running.
    /// </summary>
    public void StartDocumentFastTime()
    {
        StopAllCoroutines();
        StartCoroutine("FastTimeCoroutine");
        return;
    }

    /// <summary>
    /// Calculates the final score value gained after a stamp according to actual fever and score.
    /// </summary>
    public int CalculateFinalScoreGained(int base_score_gained)
    {
        int calculated_score;
        calculated_score = base_score_gained * actual_score_multiplier;

        Debug.Log("Gain <b>" + calculated_score + "</b> score");
        return calculated_score;
    }

    /// <summary>
    /// Calculates the actual document fast time duration.
    /// </summary>
    public float GetActualDocumentFastTimeDuration()
    {
        float calculated_value;
        calculated_value = 1 + ( GameManager.instance.DocumentsList[GameManager.instance.actualDocumentIndex].GetComponent<PaperData>().documentData.stampZonesToValidate * fast_time_duration_per_stampZone );
        
        return calculated_value;
    }
    
    /// <summary>
    /// Returns a specific rating for the current level depending on player score compared to the maximum obtainable score. 
    /// </summary>
    public enum_LevelRating CalculateLevelRating()
    {
        // good
        if (score <= maximum_score_obtainable * rating_min_percent_great)
        {
            return enum_LevelRating.Good;
        }
        // great
        else if ( score <= maximum_score_obtainable * rating_min_percent_super)
        {
            return enum_LevelRating.Great;
        }
        // super
        else if (score <= maximum_score_obtainable * rating_min_percent_perfect)
        {
            return enum_LevelRating.Super;
        }
        // perfect
        else if (score <= maximum_score_obtainable)
        {
            return enum_LevelRating.Perfect;
        }

        return enum_LevelRating.None;
    }

// = = =

// = = = [ COROUTINES ] = = =

    public IEnumerator FastTimeCoroutine()
    {
        Debug.Log("Starting fast time coroutine");
        is_in_fast_time = true;
        yield return new WaitForSeconds( GetActualDocumentFastTimeDuration() );

        Debug.Log("Fast time coroutine has ended, reseting fever");
        is_in_fast_time = false;
        FeverAmount = 0;
        yield return null;
    }

// = = =
}
