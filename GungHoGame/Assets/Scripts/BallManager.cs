using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public float minX;
    public float maxX;
    public Transform ballDrop;
    public float timeBetween = 5.0f;
    float timeStamp;
    bool ballsDropping = false;

    private void Awake()
    {

    }

    void Start ()	
	{
        timeStamp = timeBetween;
	}

	void Update ()	
	{
		
	}

    public void StartBalls()
    {
        ballsDropping = true;
        StartCoroutine(DroppingBalls());
    }

    void StartBall()
    {
        float randomX = Random.Range(minX, maxX);
        Instantiate(ballPrefab, new Vector3(randomX,ballDrop.position.y,0), Quaternion.identity);
    }

    public void StopBalls()
    {
        ballsDropping = false;
        StopCoroutine(DroppingBalls());
        BallScript[] survivingBalls = FindObjectsOfType<BallScript>();
        foreach(BallScript ball in survivingBalls)
        {
            Destroy(ball.gameObject);
        }
    }

    IEnumerator DroppingBalls()
    {
        float time = timeBetween;
        timeStamp = timeBetween;

        while(ballsDropping)
        {
            time += Time.deltaTime;
            if (time > timeStamp)
            {
                time = 0;
                timeStamp -= .1f;
                StartBall();
            }
            yield return new WaitForEndOfFrame();
        }
    }
}