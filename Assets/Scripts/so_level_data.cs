using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "LevelData", order = 1)]
public class so_level_data : ScriptableObject
{

// = = = [ VARIABLES DEFINITION ] = = =

[Space(10)][Header("Data")]
    public  int                                 id                          ;
    public  string                              label                       ;
    public  Sprite                              level_icon                  ;
    public  enum_LevelTheme                     level_theme                 ;       
    public  int                                 best_score                  ;
    public  enum_LevelRating                    level_rating                = enum_LevelRating.None;
    [Space(5)]
    public  int                                 nbr_of_documents            = 1;
    public  List<GameObject>                    level_document_types        = new List<GameObject>();

// = = =

}
