using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [Range(0,50)]
    public int startSpeed;
    [Range(0,5)]
    public int boostSpeed;

    //How far from the center 
    public float hitRange;
    //How much 
    [Range(0,90)]
    public int hitMaxAngle;

    public bool playerTwo;

    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        _rb.AddForce(transform.forward * startSpeed);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _rb.velocity *= 1.05f;
            float dist = transform.position.x - other.transform.position.x;
            int direction = other.gameObject.GetComponent<PlayerMovement>().playerNum == PlayerMovement.PlayerNumber.One ? 1 : -1;
            float angle = Mathf.Deg2Rad * hitMaxAngle * direction * dist / hitRange;
            _rb.velocity = direction * _rb.velocity.magnitude * new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
        }
        else if (other.gameObject.CompareTag("Bumper"))
        {
        }
        else
        {
        }
    }

    public void Explode()
    {
        // For now, just make the ball disappear.
        gameObject.SetActive(false);
    }
}
