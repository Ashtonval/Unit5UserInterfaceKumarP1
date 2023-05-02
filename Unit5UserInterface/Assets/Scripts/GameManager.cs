using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI pausedText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    public TextMeshProUGUI livesText;
    private float spawnRate = 1.0f;
    private int score;
    public int lives;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lives = 3;
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void UpdateLives(int livesToSubtract)
    {
        lives -= livesToSubtract;
        livesText.text = "Lives: " + lives;
    }
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        titleScreen.gameObject.SetActive(false);
        spawnRate /= difficulty;
        lives = 3;
        UpdateLives(0);
        UpdateScore(0);
    }
    public void Paused()
    {
        Time.timeScale = 0;
        pausedText.gameObject.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
        pausedText.gameObject.SetActive(false);
    }
    




    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Paused();
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            Resume();
        }
    }
}
