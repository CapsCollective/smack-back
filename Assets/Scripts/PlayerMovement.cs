using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerNumber { One=1, Two=2};
    public PlayerNumber playerNum = PlayerNumber.One;
    public float Hspeed = 1f;
    public float Vspeed = 0.75f;
    public float offsetAddition = 0.5f;
    public float forceMag = 10f;

    private Rigidbody RB;
    private Vector3 movement;
    private bool move = true;
    private AudioSource AS;
    public AudioClip[] clips;

    private void Awake()
    {
        movement = Vector3.zero;
        RB = gameObject.GetComponent<Rigidbody>();
        AS = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        AS.clip = clips[(int)playerNum - 1]; 
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Move((int)playerNum);
        }
        
    }

    private void FixedUpdate()
    {
    }

    private void Move(int num)
    {
        string Hname = "Hor" + num;
        string Vname = "Ver" + num;

        if (Input.GetAxisRaw(Hname) != 0)
        {
            movement.x = Input.GetAxisRaw(Hname) * Hspeed * Time.deltaTime;
        }
        else
        {
            movement.x = 0;
        }

        if (Input.GetAxisRaw(Vname) != 0)
        {
            movement.z = Input.GetAxisRaw(Vname) * Hspeed * Time.deltaTime;
        }
        else
        {
            movement.z = 0;
        }

        gameObject.transform.Translate(movement);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bumper")
        {
            Vector3 forceDir = new Vector3(-collision.transform.position.x + gameObject.transform.position.x, 0, -collision.transform.position.z + gameObject.transform.position.z).normalized;
            RB.AddForce(forceDir*forceMag);
            collision.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            collision.gameObject.GetComponent<AudioSource>().Play();
            StartCoroutine(Bump());
            
        }

        else if(collision.gameObject.tag == "Ball")
        {
            AS.Play();
        }
    }

    IEnumerator Bump()
    {
        move = false;
        yield return new WaitForSeconds(0.5f);
        move = true;
    }

    public void SetMakeMove(bool i)
    {
        move = i;
    }
}
