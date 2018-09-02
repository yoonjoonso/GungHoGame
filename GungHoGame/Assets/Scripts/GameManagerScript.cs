using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject scoreBoard;
    public GameObject endScreen;
    public GameObject playerPrefab;
    public GameObject ballPrefabs;
    public Transform startArea;

    static GameObject playerObject;
    static Vector2 startPosition;
    static int score = 0;
    int highscore;
    static Text scoreText;

    enum GameState { menu, game, end };
    GameState state;

    private void Awake()
    {
        state = GameState.menu;
        scoreText = scoreBoard.GetComponent<Text>();
        startPosition = startArea.position;
    }

    void Start ()
    {

	}
	
	void Update ()
    {
        if (state == GameState.menu && Input.GetButtonDown("Punch")) //punch is space for now in the input
        {
            state = GameState.game;
            StartCoroutine(StartGame());
        }
	}

    public static void ResetPlayerPosition()
    {
        playerObject.transform.position = startPosition;
    }

    public static void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    IEnumerator StartGame()
    {
        playerObject = Instantiate(playerPrefab, startPosition, Quaternion.identity);
        yield return 0;
    }
}
