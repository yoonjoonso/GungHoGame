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
    Rigidbody2D rb;
    Collider2D coll;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
        Move(Input.GetAxis("Horizontal"));

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if(Input.GetButtonDown("Punch"))
        {
            Punch();
        }
    }

    void Move(float direction)
    {
        if (Mathf.Abs(direction) > .5)
        {
            transform.eulerAngles = new Vector3(0, 180 - (direction * rotateAmount), 0);

            if (Grounded())
            {
                var clipinfos = animator.GetCurrentAnimatorClipInfo(0);
                //if(clipinfos[0].clip.name != "Punch")
                animator.Play("Walk");
            }

            direction *= verticalSpeed;
            Vector3 originalVel = rb.velocity;
            rb.velocity = new Vector3(direction, originalVel.y, 0);
        }
    }

    void Jump()
    {
        if (Grounded())
        {
            animator.Play("Jump");
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode2D.Impulse);
        }
    }

    void Punch()
    {
        animator.Play("Punch");
    }

    bool Grounded()
    {
        RaycastHit2D rayhit = Physics2D.Raycast(new Vector3(0, coll.bounds.min.y, 0) , Vector3.down, .2f);

        if(rayhit)
            return rayhit.transform.tag == "Floor";
        else
            return false;
    }
}
