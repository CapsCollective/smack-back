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

    private Collider col;
    private Vector3 movement;

    private void Awake()
    {
        movement = Vector3.zero;
        col = gameObject.GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move((int)playerNum);
    }

    private void Move(int num)
    {
        //movement = Vector3.zero;

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
        if (collision.gameObject.tag == "Ball")
        {
            // get the location of the collision
            // Find hor component of vector
            // Apply force to ball in that direction
        }
    }
}
