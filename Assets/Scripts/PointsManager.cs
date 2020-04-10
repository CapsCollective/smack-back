using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Points Manager", menuName = "Points Manager", order = 50)]
public class PointsManager : ScriptableObject
{
    public GameEvent onScore;

    public int Points { get; private set; }

    public void Score(int points)
    {
        Points += points;

        onScore.Raise();
    }
}
