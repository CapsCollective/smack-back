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
    private MeshRenderer _mr;
    private AudioSource _audio;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _audio = gameObject.GetComponent<AudioSource>();
        _rb = gameObject.GetComponent<Rigidbody>();
        _mr = GetComponent<MeshRenderer>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (_rb.velocity.magnitude < 7.5f) _rb.velocity = _rb.velocity.normalized * 7.5f;
        if (other.gameObject.CompareTag("Player"))
        {
            float dist = transform.position.x - other.transform.position.x;
            int direction = other.gameObject.GetComponent<PlayerMovement>().playerNum == PlayerMovement.PlayerNumber.One ? 1 : -1;
            float angle = Mathf.Deg2Rad * hitMaxAngle * direction * dist / hitRange;
            _rb.velocity = direction * _rb.velocity.magnitude * new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
            _rb.velocity += _rb.velocity.normalized * 0.4f;
        }
        else
        {
            _audio.Play();
        }

    }

    public void MakeVisible()
    {
        _mr.enabled = true;
    }

    public void StartMoving()
    {
        enabled = true;
        _rb.isKinematic = false;
        _rb.AddForce(transform.forward * startSpeed);
    }

    public void Explode()
    {
        _particleSystem.Play();
        enabled = false;
        _mr.enabled = false;
        _rb.isKinematic = true;
    }
}
