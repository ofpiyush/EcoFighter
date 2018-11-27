using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject HealthBar;
    public GameObject Enemy;
    public static GameManager instance;


    public float PollutionRate = 0f;
    public float SunMultiplier = 0f;

    public float bushPollutionKill = 0.2f;

    public float factoryPollutionMake = 1f;

    
    public float PollutionPercentage = 0f;
    float pollutionLevel = 0f;

    public float pollutionBackgroundRate = 1.5f;

    public float MaxPollutionLevel = 500f;

    bool isEnemySpawned;

    void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
        else if (GameManager.instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        pollutionLevel = MaxPollutionLevel/4f;
    }

    void FixedUpdate()
    {
		if(PauseMenu.IsPaused) {
			return;
		}

        CalcPollutionLevel();
        if(!isEnemySpawned) {
            CheckSpawnEnemy();
        }
    }


    void CheckSpawnEnemy() {
        if(PollutionPercentage > 0.2f) {
            return;
        }
        Enemy.SetActive(true);
        isEnemySpawned = true;
    }

    public void GameOver() {
        ResetPollution();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CalcPollutionLevel() {

        float PollutionPerSec = pollutionBackgroundRate + 
            (GameObject.FindGameObjectsWithTag("Enemy").Length*factoryPollutionMake) - 
            (GameObject.FindGameObjectsWithTag("Vegetation").Length*bushPollutionKill);
        PollutionRate = PollutionPerSec/MaxPollutionLevel;
        Debug.Log(PollutionRate);
        AddPollution(PollutionPerSec * Time.fixedDeltaTime);
    }

    void ResetPollution() {
        AddPollution(-pollutionLevel);
    }
    void AddPollution(float level) {
        pollutionLevel = Mathf.Clamp(pollutionLevel + level, 0f, MaxPollutionLevel);
        PollutionPercentage = pollutionLevel/MaxPollutionLevel;
    }
}
