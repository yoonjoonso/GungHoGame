using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreWallScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ball")
        {
            GameManagerScript.instance.AddScore();
            Destroy(other.gameObject);
        }

        //failsafe in case player gets out
        if(other.tag == "Player")
        {
            GameManagerScript.ResetPlayerPosition();
        }
    }
}
