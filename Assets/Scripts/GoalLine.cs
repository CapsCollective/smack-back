using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLine : MonoBehaviour
{
    [Range(0f, 1f)] public float majorRatio;
    public GameEvent majorEvent;
    public GameEvent minorEvent;
    public GameEvent reset;

    private static readonly string BallTag = "Ball";

    private void Start()
    {
        reset.Raise();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(BallTag))
        {
            float hitRatio = Mathf.Abs(transform.worldToLocalMatrix.MultiplyPoint(collision.contacts[0].point).x);

            if (hitRatio < 0.5f * majorRatio)
                majorEvent.Raise();
            else
                minorEvent.Raise();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.Scale(transform.localScale, new Vector3(majorRatio, 1, 1)));
    }
}
