using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New StageData", menuName = "StageData")]
public class StageData : ScriptableObject
{
    public Vector2 limitMin;
    public Vector2 limitMax;

    public Vector2 LimitMin => limitMin;
    public Vector2 LimitMax => limitMax;
}
