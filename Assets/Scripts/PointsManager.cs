using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Points Manager", menuName = "Points Manager", order = 50)]
public class PointsManager : ScriptableObject
{
    public int maxScore;
    
    public int Points { get; private set; }
    public bool Max { get { return Points >= maxScore; } }

    public void Score(int points)
    {
        Points += points;
    }

    public void Reset()
    {
        Points = 0;
    }
}
