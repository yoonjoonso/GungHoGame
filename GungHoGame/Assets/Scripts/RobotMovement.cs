using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]

public class RobotMovement : MonoBehaviour
{
    public float verticalSpeed = .5f;
    public float jumpSpeed = 1;
    public float rotateAmount;
    public GameObject punchBoxSpace;
    PunchBox punchBox;
    public Transform punchArea;

    Rigidbody2D rb;
    Collider2D coll;
    Animator animator;
    bool canPunch = false;
    bool canJump = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    void Start ()
    {
        punchBox = punchBoxSpace.GetComponent<PunchBox>();
	}
	
	void Update ()
    {
        //This is to have a moving collider that's not childed
        punchBoxSpace.transform.position = punchArea.position;

        Move(Input.GetAxis("Horizontal"));

        //Check if I can punch or jump
        canJump = !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump");
        canPunch = !animator.GetCurrentAnimatorStateInfo(0).IsName("Punch");

        if (Input.GetButtonDown("Punch"))
        {
            Punch();
        }

        //Only jump if I am grounded
        if (Grounded())
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
    }

    void Move(float direction)
    {
        if (Mathf.Abs(direction) > 0.1f)
        {
            animator.SetBool("IsWalking", true);

            int rounded = 0;
            if (direction > 0)
                rounded = 1;
            if (direction < 0)
                rounded = -1;

            transform.eulerAngles = new Vector3(0, 180 - (rounded * rotateAmount), 0);
            direction *= verticalSpeed;
            Vector3 originalVel = rb.velocity;
            rb.velocity = new Vector3(direction, originalVel.y, 0);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    void Jump()
    {
        if (canJump)
        {
            animator.SetTrigger("Jumped");
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode2D.Impulse);
            canJump = false;
        }
    }

    void Punch()
    {
        if (canPunch)
        {
            animator.SetTrigger("Punched");
            canPunch = false;
            punchBox.Punch();
        }
    }

    //Raycasts downwards to check for grounding
    bool Grounded()
    {
        RaycastHit2D rayhit = Physics2D.Raycast(new Vector3(0, coll.bounds.min.y, 0) , Vector3.down, .1f);

        if(rayhit)
            return rayhit.transform.tag == "Floor";
        else
            return false;
    }
}
