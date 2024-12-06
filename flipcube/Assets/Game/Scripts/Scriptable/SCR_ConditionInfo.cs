using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Condition", menuName = "Condition/Game Condition")]
public class SCR_ConditionInfo : ScriptableObject
{
    [Header("Requirements")]
    [SerializeField] int redPoints = -1;
    [SerializeField] int bluePoints = -1;
    [SerializeField] int greenPoints = -1;
    [SerializeField] int yellowPoints = -1;
    [SerializeField] int maxMoviments = -1;
    [SerializeField] float maxTimer = -1;

    public int GetRequiredRedPoints()
    {
        return redPoints;
    }
    public int GetRequiredBluePoints()
    {
        return bluePoints;
    }
    public int GetRequiredGreenPoints()
    {
        return greenPoints;
    }
    public int GetRequiredYellowPoints()
    {
        return yellowPoints;
    }
    public int GetMaxMoviments()
    {
        return maxMoviments;
    }
    public float GetMaxTimer()
    {
        return maxTimer;
    }
}
