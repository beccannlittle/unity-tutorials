using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField]
    private GameObject hazardPrefab;
    [SerializeField]
    private Vector3 spawnValues;
    [SerializeField]
    private int hazardCount;
    [SerializeField]
    private float spawnWait, startWait, waveWait;

    public Text scoreText, restartText, gameOverText;
    private bool gameOver, restart;
    private int score;
	
    private void Start()
    {
        gameOver = false;
        restart = false;
        gameOverText.text = "";
        restartText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

	private void Update()
	{
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
	}

	private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazardPrefab, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateScore();
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }
}
