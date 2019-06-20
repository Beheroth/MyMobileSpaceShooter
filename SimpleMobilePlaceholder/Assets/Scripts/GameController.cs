using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int hazardCount;
    public GameObject[] hazards;
    public Vector3 spawnValues;

    public GameObject boss;
    public int bossWave;
    private bool bossFight = false;

    public float spawnWait;
    public float startWait;

    public delegate void ChangeAction();
    public static event ChangeAction OnChange;
    private int waveCounter;

    public GameObject[] drops;

    public Text scoreText;
    private int score;

    private bool gameOver;

    public void setBossFight(bool bossFight)
    {
        this.bossFight = bossFight;
    }

    public int getScore()
    {
        return this.score;
    }

    public int getWaveCounter()
    {
        return this.waveCounter;
    }

    public bool getGameOver()
    {
        return this.gameOver;
    }

    void Start()
    {
        score = 0;
        waveCounter = 0;
        if(bossWave < 1)
        {
            bossWave = 1;
        }
        gameOver = false;
        StartCoroutine (SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);  //coroutines
        //Debug.Log("bossfight " + bossFight);
        while (true)
        {
            yield return new WaitUntil(() => !bossFight);   //coroutine
            WaveIncrement();
            Debug.Log("Wave#" + waveCounter.ToString() + " bossFight " + bossFight);
            if (waveCounter % bossWave == 0)
            {
                bossFight = true;
                Vector3 bossSpawn = new Vector3(0f, spawnValues.y, spawnValues.z);
                yield return new WaitForSeconds(startWait);  //coroutines
                Instantiate(boss, bossSpawn, Quaternion.identity);
            }
            else
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                    Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(1 + (spawnWait / waveCounter));  //coroutines
                }
            }
            if (gameOver)
            {
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue * (1 + (waveCounter - 1) / bossWave);
        //UpdateScoreText();
        if (OnChange != null)
        {
            OnChange();
        }
    }
    public void WaveIncrement()
    {
        waveCounter += 1;
        if (OnChange != null)
        {
            OnChange();
        }
    }
    
    public GameObject RollDrop(float dropRate)
    {
        GameObject drop = null;
        if(Random.Range(0f, 1f) <= dropRate)
        {
            drop = drops[Random.Range(0, drops.Length)];
        }
        return drop;
    }

    public void setGameOver(bool gameOver)
    {
        if(gameOver != this.gameOver)
        {
            this.gameOver = gameOver;
            if (OnChange != null)
            {
                OnChange();
            }
        }
    }

    public void Restart()
    {
        Debug.Log("Trying to restart");
        if (gameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

