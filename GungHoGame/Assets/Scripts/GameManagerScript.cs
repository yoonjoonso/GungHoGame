using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    static GameManagerScript GMinstance = null;

    public static GameManagerScript instance
    {
        get
        {
            if (GMinstance == null)
            {
                GMinstance = FindObjectOfType(typeof(GameManagerScript)) as GameManagerScript;
            }

            if(GMinstance == null)
            {
                GameObject obj = new GameObject("GameManager");
                GMinstance = obj.AddComponent<GameManagerScript>() as GameManagerScript;
            }
            return GMinstance;
        }
    }

    public GameObject startScreen;
    public GameObject scoreBoard;
    public GameObject endScreen;
    public GameObject playerPrefab;
    public BallManager ballmanager;
    public Transform startArea;
    public Text highScoreText;

    static GameObject playerObject;
    static Vector2 startPosition;
    int score = 0;
    int highscore = 0;
    static Text scoreText;

    enum GameState { menu, game, end };
    static GameState state;

    private void Awake()
    {
        state = GameState.menu;
        scoreText = scoreBoard.GetComponent<Text>();
        startPosition = startArea.position;
    }

    void Start ()
    {
        scoreBoard.SetActive(false);
        startScreen.SetActive(true);
    }
	
	void Update ()
    {
        //when the game starts
        if (state == GameState.menu && Input.GetButtonDown("Punch")) //punch is space for now in the input
        {
            state = GameState.game;
            playerObject = Instantiate(playerPrefab, startPosition, Quaternion.identity);
            scoreBoard.SetActive(true);
            ballmanager.StartBalls();
            startScreen.SetActive(false);
        }

        //when the game goes back to menu
        if(state == GameState.end && Input.GetKeyDown(KeyCode.R))
        {
            state = GameState.menu;
            startScreen.SetActive(true);
            endScreen.SetActive(false);
            scoreBoard.SetActive(false);
        }
	}

    public static void ResetPlayerPosition()
    {
        playerObject.transform.position = startPosition;
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void KillPlayer()
    {
        state = GameState.end;
        Destroy(playerObject);
        ballmanager.StopBalls();
        if (score > highscore)
            highscore = score;
        highScoreText.text = "High Score: " + highscore;
        endScreen.SetActive(true);
        score = 0;
        scoreText.text = "Score: 0";
    }
}
