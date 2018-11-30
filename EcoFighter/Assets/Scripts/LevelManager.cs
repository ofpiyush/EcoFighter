using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI Remaining;
    public GameObject Enemy;

    public GameObject Player;

    public float TotalGameTime = 300f;
    public float EnemyComesAt = 60f;
    public static float RemainingGameTime = 300f;
    public float bushPollutionKill = 0.2f;
    public float factoryPollutionMake = 1f;
    float pollutionLevel = 0f;
    public float pollutionBackgroundRate = 1.5f;
    public float MaxPollutionLevel = 500f;
    bool isEnemySpawned;
    float timer;


    void Start() {
        pollutionLevel = MaxPollutionLevel/4f;
        isEnemySpawned = false;
    }

    void FixedUpdate()
    {
		if(PauseMenu.IsPaused) {
			return;
		}

        CalcPollutionLevel();
        RemainingGameTime -= Time.deltaTime;
        Remaining.text = ((int) RemainingGameTime).ToString();

        if(!isEnemySpawned) {
            CheckSpawnEnemy();
        }

        if(RemainingGameTime <= 0f || pollutionLevel >= MaxPollutionLevel) {
            GameOver();
        }

        if(pollutionLevel <= 0f && isEnemySpawned && Enemy == null) {
            //Todo: load some end credits
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        }
    }

    void SpawnGoods() {

    } 

    void CheckSpawnEnemy() {
        if(TotalGameTime - RemainingGameTime < EnemyComesAt) {
            return;
        }
        Enemy.SetActive(true);
        isEnemySpawned = true;
    }

    public void GameOver() {
        ResetPollution();
        isEnemySpawned = false;
        RemainingGameTime = TotalGameTime;
        pollutionLevel = MaxPollutionLevel/4f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CalcPollutionLevel() {

        float PollutionPerSec = pollutionBackgroundRate + 
            (GameObject.FindGameObjectsWithTag("Enemy").Length*factoryPollutionMake) - 
            (GameObject.FindGameObjectsWithTag("Vegetation").Length*bushPollutionKill);

        AddPollution(PollutionPerSec * Time.fixedDeltaTime);
    }

    void ResetPollution() {
        AddPollution(-pollutionLevel);
    }
    void AddPollution(float level) {
        pollutionLevel = Mathf.Clamp(pollutionLevel + level, 0f, MaxPollutionLevel);
        GameManager.instance.PollutionPercentage = pollutionLevel/MaxPollutionLevel;
    }
}
