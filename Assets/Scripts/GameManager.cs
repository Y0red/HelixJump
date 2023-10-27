using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager singleton;
    public int best;
    public int score;
    public int currentStage = 0;
    
    [SerializeField]GameObject startMenu, endMenu;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        // Load the saved highscore
        best = PlayerPrefs.GetInt("Highscore");
    }
    public void NextLevel()
    {
        currentStage++;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
    }
    public void RestartLevel()
    {
       // Debug.Log("Restarting Level");
        // Show Adds Advertisement.Show();
        singleton.score = 0;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
        endMenu.SetActive(false);
        
    }
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if (score > best)
        {
            PlayerPrefs.SetInt("Highscore", score);
            best = score;
        }
    }
    public void StartGame()
    {
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
        startMenu.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void EndGame()
    {
        endMenu.SetActive(true);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
