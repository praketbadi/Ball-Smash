using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float gameDuration = 30f;
    private float timer;
    private bool gameIsOver = false;
    public Text timerDisplay;
    public GameObject gameover; // Renamed to match your project
    public Text finalScoreText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        timer = gameDuration;
        gameIsOver = false;
        UpdateTimerDisplay();
        if (gameover != null)
        {
            gameover.SetActive(false);
        }
        else
        {
            Debug.LogError("GameObject named 'gameover' not assigned in the GameManager!");
        }
    }

    void Update()
    {
        if (!gameIsOver)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay();

            if (timer <= 0f)
            {
                EndGame();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerDisplay != null)
        {
            timerDisplay.text = "Time: " + Mathf.CeilToInt(timer).ToString();
        }
        else
        {
            Debug.LogWarning("Timer Display Text component not assigned in the GameManager!");
        }
    }

    public void EndGame()
    {
        if (!gameIsOver)
        {
            gameIsOver = true;
            Debug.Log("Game Over!");

            if (gameover != null)
            {
                gameover.SetActive(true);

                if (finalScoreText != null && scoreman.instance != null)
                {
                    finalScoreText.text = "Final Score: " + scoreman.instance.score.ToString();
                }
                else if (finalScoreText == null)
                {
                    Debug.LogWarning("FinalScoreText not assigned in the GameManager!");
                }
                else if (scoreman.instance == null)
                {
                    Debug.LogError("scoreman Instance is null! Make sure the scoreman script is present in the scene.");
                }
            }
            else
            {
                Debug.LogError("GameObject named 'gameover' not assigned in the GameManager!");
            }

            // Optional: Stop game time
            // Time.timeScale = 0f;
        }
    }

    public bool IsGameOver()
    {
        return gameIsOver;
    }

    public void RestartGame()
    {
        gameIsOver = false;
        timer = gameDuration;
        if (scoreman.instance != null)
        {
            scoreman.instance.score = 0; // Reset the score in scoreman
            scoreman.instance.UpdateScoreUI();
        }
        UpdateTimerDisplay();
        if (gameover != null)
        {
            gameover.SetActive(false);
        }
        // Optional: Resume game time
        // Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}