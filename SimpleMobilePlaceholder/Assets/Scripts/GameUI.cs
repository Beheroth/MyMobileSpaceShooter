using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text scoreText;
    public Text waveText;
    public GameObject gameOverObject;

    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("GameUI.Start()");
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Couldn't find 'GameController' script");
        }
        GameController.OnChange += UpdateScoreText;
        GameController.OnChange += UpdateWaveText;
        GameController.OnChange += UpdateGameOverUI;

    }

    private void UpdateScoreText()
    {
        int score = gameController.getScore();
        scoreText.text = "Score: " + score.ToString();
    }

    private void UpdateWaveText()
    {
        int waveCounter = gameController.getWaveCounter();
        waveText.text = "Wave: " + waveCounter.ToString();
    }

    private void UpdateGameOverUI()
    {
        bool gameOver = gameController.getGameOver();
        if(gameOverObject != null)
        {
            gameOverObject.SetActive(gameOver);
        }
    }

    private void Unsubscribe()
    {
        GameController.OnChange -= UpdateScoreText;
        GameController.OnChange -= UpdateWaveText;
        GameController.OnChange -= UpdateGameOverUI;
    }

    public void RestartButton()
    {
        //Unsubscribe();
        gameController.Restart();
    }

    private void PostToDataBase()
    {
        //
    }
}
