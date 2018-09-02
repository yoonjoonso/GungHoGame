using System.Collections;
using System.Collections.Generic;using UnityEngine;

public class PunchBox : MonoBehaviour
{
    public int punchPower;

	void Start ()
    {		

	}
    
	void Update ()
    {

	}

    public void Punch()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale, 0);
        foreach(Collider2D coll in colliders)
        {
            if (coll.tag == "Ball")
            {
                Vector2 direction = coll.transform.position - transform.position;
                if (direction.y < 0)
                    direction.y = .3f;
                coll.GetComponent<Rigidbody2D>().AddForce(direction * punchPower);
            }
        }
    }
}
