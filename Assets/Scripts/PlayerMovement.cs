using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator; 
    [SerializeField] private AudioSource footstepsSound;

    public enum PlayerNumber { One = 1, Two = 2 };
    public PlayerNumber playerNum = PlayerNumber.One;
    public float Hspeed = 1f;
    public float Vspeed = 0.75f;
    public float offsetAddition = 0.5f;
    public float forceMag = 10f;


    // AI Controllers
    public Vector3 defaultPos;
    public bool aiControlled = false;
    public Transform ballTransform;
    public Rigidbody ballRb;
    
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
        defaultPos = transform.position;
    }

    private void Start()
    {
        AS.clip = clips[(int)playerNum - 1];
        if (playerNum == PlayerNumber.Two) aiControlled = !ServiceLocator.Current.Get<PlayerManager>().twoPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            if (aiControlled) AutoMove();
            else Move((int) playerNum);
        }
        else
        {
            animator.SetFloat("Blend X", 0);
            animator.SetFloat("Blend Y", 0);
        }
    }

    private void Move(int num)
    {
        string Hname = "Hor" + num;
        string Vname = "Ver" + num;
        var is_moving = false;

        if (Input.GetAxisRaw(Hname) != 0)
        {
            movement.x = Input.GetAxisRaw(Hname) * Hspeed * Time.deltaTime;
            is_moving = true;
        }
        else
        {
            movement.x = 0;
        }

        if (Input.GetAxisRaw(Vname) != 0)
        {
            movement.z = Input.GetAxisRaw(Vname) * Hspeed * Time.deltaTime;
            is_moving = true;
        }
        else
        {
            movement.z = 0;
        }

        if (is_moving && !footstepsSound.isPlaying)
            footstepsSound.Play();
        else if (!is_moving && footstepsSound.isPlaying)
            footstepsSound.Stop();
        else
            footstepsSound.pitch = Random.Range(0.70f, 1.30f);
            
        animator.SetFloat("Blend X", Input.GetAxis(Hname));
        animator.SetFloat("Blend Y", Input.GetAxis(Vname));
        transform.Translate(movement);

    }

    private void AutoMove()
    {
        Vector3 target;
        if (ballRb.velocity.z > 0)
        {
            target = ballTransform.position; // Remove the y axis
            target.y = 0;
            if (target.z < 0) target.z = 1;
            else target.z += 1;
        }
        else
        {
            target = defaultPos;
        }

        Vector3 diff = Vector3.zero;
        bool is_moving = false;
        if (Vector3.Distance(target, transform.position) > 0.5f)
        {
            Vector3 movement = Vector3.MoveTowards(transform.position, target, 6 * Time.deltaTime);
            diff = (transform.position - movement) / (8 * Time.deltaTime);

            transform.position = movement;
            is_moving = true;
        }
        
        if (is_moving && !footstepsSound.isPlaying)
            footstepsSound.Play();
        else if (!is_moving && footstepsSound.isPlaying)
            footstepsSound.Stop();
        else
            footstepsSound.pitch = Random.Range(0.70f, 1.30f);
        
        animator.SetFloat("Blend X", diff.x);
        animator.SetFloat("Blend Y", diff.z);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bumper")
        {
            Vector3 forceDir = new Vector3(-collision.transform.position.x + gameObject.transform.position.x, 0, -collision.transform.position.z + gameObject.transform.position.z).normalized;
            RB.AddForce(forceDir*forceMag);
            collision.gameObject.GetComponent<BumperEffect>().Play();
            StartCoroutine(Bump());
        }

        else if(collision.gameObject.tag == "Ball")
        {
            AS.Play();
            animator.SetTrigger("Hit");
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
