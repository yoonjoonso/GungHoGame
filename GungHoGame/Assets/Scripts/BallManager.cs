using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallManager : MonoBehaviour
{
    public GameObject ballPrefab;
    List<GameObject> liveballs;
    Queue<GameObject> deadballs;

    private void Awake()
    {
        liveballs = new List<GameObject>();
        deadballs = new Queue<GameObject>();
    }

    void Start ()	
	{
		
	}

	void Update ()	
	{
		
	}

    public void StartBalls()
    {

    }

    void StartBall(GameObject ballObj)
    {

    }

    public void StopBalls()
    {

    }

    void StopBall(GameObject ballObj)
    {

    }
}