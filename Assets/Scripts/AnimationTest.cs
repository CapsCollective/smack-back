using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Blend X", Input.GetAxis("Hor1"));
        animator.SetFloat("Blend Y", Input.GetAxis("Ver1"));
        if(Input.GetKeyDown(KeyCode.Space))
            animator.SetTrigger("Hit");
    }
}
