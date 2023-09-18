using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For using UI elements
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameIsActive = true;
    public int lives = 5;
    public int score = 0;
    public Text livesText; // Reference to Text component for displaying lives
    public Text scoreText; // Reference to Text component for displaying score
    public Text timerText; // Reference to Text component for displaying timer
    public GameObject gameOverCanvas; // Reference to the Game Over Canvas
    public Text finalScoreText;
    public PlayerBehavior pb;

    private float startTime;
    private bool gameEnded = false;

    void Start()
    {
        // Initialize timer
        startTime = Time.time;
        UpdateUI();
    }

    void Update()
    {
        if (gameIsActive) {
            if (!gameEnded)
            {
                if (pb.hasStarted) {
                // Update timer
                float t = Time.time - startTime;
                string minutes = ((int)t / 60).ToString();
                string seconds = (t % 60).ToString("f2");
                timerText.text = "Time: " + minutes + ":" + seconds;
                }
            }
        }
        else
        {
            // Listen for a specific key press to restart the game
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Reload the current scene to restart the game
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void BlockHit()
    {
        // Update score
        score += 100;
        UpdateUI();
    }

    public void LoseLife()
    {
        // Update lives
        lives--;
        UpdateUI();

        if (lives <= 0)
        {
            EndGame();
        }
    }

    private void UpdateUI()
    {
        // Update UI elements
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;

    }
    public void GameOver() {
        gameOverCanvas.SetActive(true);
        finalScoreText.text = "Final score " + score;
    }

    private void EndGame()
    {
        gameEnded = true;
        gameIsActive = false;
        GameOver();

        // Calculate final score based on time
        float timeTaken = Time.time - startTime;
        float timeFactor = Mathf.Max(0, 1 - timeTaken / 300); // 300 seconds = 5 minutes
        int finalScore = Mathf.FloorToInt(score * (1 + timeFactor));



        // Display final score (you might want to navigate to a Game Over screen instead)
        //scoreText.text = "Final Score: " + finalScore;



         Invoke("LoadTitleScreen", 3f);
    }

    private void LoadTitleScreen()
    {
        SceneManager.LoadScene("Title Screen");  // Replace with your title screen scene name
    }
}

