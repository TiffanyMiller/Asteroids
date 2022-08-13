using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;
    private int _score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    private void Awake()
    {
        inst = this;
        
        gameOverText.gameObject.SetActive(false);
        UpdateScore(0);
    }

    public void UpdateScore(int points)
    {
        _score += points;
        scoreText.text = ScoreString;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.text += ScoreString;
        scoreText.text = "";

        Time.timeScale = 0;
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        Time.timeScale = 1;
    }

    private string ScoreString => _score.ToString("00");
}
