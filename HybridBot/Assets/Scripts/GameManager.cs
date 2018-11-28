using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject HealthBar;
    public float SunMultiplier = 0f;
    public float PollutionPercentage = 0f;

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


    public void GameOver() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
