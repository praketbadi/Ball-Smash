using UnityEngine;
using UnityEngine.UI;

public class scoreman : MonoBehaviour
{
    public static scoreman instance;
    public Text scoreText;
    public Text highscoreText;

   public  int score = 0;
   public  int highscore = 0;

    void Awake()
    {
        // Singleton implementation
        if (instance == null)
        {
            instance = this;
            // Optional: DontDestroyOnLoad(gameObject); // Keep score across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return; // Ensure the rest of Awake doesn't execute for the duplicate
        }
    }

    void Start()
    {
        UpdateScoreUI();
        LoadHighscore(); // Load highscore at the start
    }

    public void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        UpdateScoreUI();
        CheckForHighscore();
    }

    public void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString() + " :POINTS";
        }
    }

    void LoadHighscore()
    {
        highscore = PlayerPrefs.GetInt("Highscore", 0);
        UpdateHighscoreUI();
    }

    void SaveHighscore()
    {
        PlayerPrefs.SetInt("Highscore", highscore);
        PlayerPrefs.Save();
    }

    void CheckForHighscore()
    {
        if (score > highscore)
        {
            highscore = score;
            UpdateHighscoreUI();
            SaveHighscore();
        }
    }

    void UpdateHighscoreUI()
    {
        if (highscoreText != null)
        {
            highscoreText.text = "HIGHSCORE: " + highscore.ToString();
        }
    }
}