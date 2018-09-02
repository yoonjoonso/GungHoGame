using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreWallScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ball")
        {
            GameManagerScript.AddScore();
            Destroy(other.gameObject);
        }
    }
}
