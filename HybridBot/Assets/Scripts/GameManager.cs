using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject HealthBar;
    public static GameManager instance;
    public float SunMultiplier = 0f;

    public float bushPollutionKill = 0.2f;

    public float factoryPollutionMake = 1f;

    
    public float PollutionPercentage = 0f;
    float pollutionLevel = 0f;

    public float pollutionBackgroundRate = 1.5f;

    public float MaxPollutionLevel = 500f;

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

    void FixedUpdate()
    {
        CalcPollutionLevel();
    }


    public void GameOver() {
        ResetPollution();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CalcPollutionLevel() {
        AddPollution(
        (
            pollutionBackgroundRate + 
            (GameObject.FindGameObjectsWithTag("Enemy").Length*factoryPollutionMake) - 
            (GameObject.FindGameObjectsWithTag("Vegetation").Length*bushPollutionKill)
            ) * Time.fixedDeltaTime
        );
    }

    void ResetPollution() {
        AddPollution(-pollutionLevel);
    }
    void AddPollution(float level) {
        pollutionLevel = Mathf.Clamp(pollutionLevel + level, 0f, MaxPollutionLevel);
        PollutionPercentage = pollutionLevel/MaxPollutionLevel;
    }
}
