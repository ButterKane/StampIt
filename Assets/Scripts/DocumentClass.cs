using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DocumentClass
{
    public int numberOfStampsToInstantiate;

    public int stampZonesToValidate;
    public int stampZonesValidated;
    public Sprite DocumentApparence;

    public List<Vector3> stampingZonesLocations;

    public List<StampClass> stampZonesToGenerate;

    public int localScore;

    

}
