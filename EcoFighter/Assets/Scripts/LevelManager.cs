using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : Gameplay
{
    public TextMeshProUGUI Remaining;
    public TextMeshProUGUI KilledEnemy;
    public TextMeshProUGUI PollutionZero;
    public TextMeshProUGUI DeathReason;
    public GameObject GameOverScreen;
    public GameObject Player;
    public GameObject Suffocating;
    public GameObject LowHealth;
    public GameObject LowCharge;
    public GameObject WeWonScene;
    public GameObject PauseUI;
    public GameObject Enemy;

    public float TotalGameTime = 300f;
    public float EnemyComesAt = 60f;
    public static float RemainingGameTime = 300f;
    public float bushPollutionKill = 0.2f;
    public float factoryPollutionMake = 1f;
    float pollutionLevel = 0f;
    public float pollutionBackgroundRate = 1.5f;
    public float MaxPollutionLevel = 500f;

    GameObject[] AllBushes;

    float lastSpawnInvoked = 0f;
    float lastHeartSpawned = 0f;
    float lasSeedSpawned = 0f;
    bool isEnemySpawned;
    float timer;
    bool pollutionFixed;

    Health playerHealth;
    Charger playerCharge;

    public Spawnable Heart;
    public Spawnable Seed;

    bool IsLowHealth;
    bool IsLowCharge;
    bool IsSuffocating;

    int HeartCount = 0;
    int SeedCount = 0;



    void Start() {
        RemainingGameTime =  300f;
        lastSpawnInvoked = RemainingGameTime;
        lastHeartSpawned = RemainingGameTime;
        lasSeedSpawned = RemainingGameTime;
        pollutionLevel = MaxPollutionLevel/4f;
        IsGameEnded = false;
        IsPaused = false;
        playerHealth = Player.GetComponent<Health>();
        playerCharge = Player.GetComponent<Charger>();


        isEnemySpawned = false;
        pollutionFixed = false;
        IsLowCharge = false;
        IsLowHealth = false;
        IsLowCharge = false;
        HeartCount = 0;
        SeedCount = 0;
        LowHealth.SetActive(false);
        LowCharge.SetActive(false);
        Suffocating.SetActive(false);
        PollutionZero.text="[ ]";
        KilledEnemy.text="[ ]";
    }

    void FixedUpdate()
    {
		if(Gameplay.IsPaused) {
			return;
		}

        CalcPollutionLevel();
        RemainingGameTime -= Time.deltaTime;
        Remaining.text = ((int) RemainingGameTime).ToString();

        if(!isEnemySpawned) {
            CheckSpawnEnemy();
        }

        if(IsLowHealth) {
            if(playerHealth.HealthPercent > 0.2f) {
                LowHealth.SetActive(false);
                IsLowHealth = false;
            }
        } else {
            if(playerHealth.HealthPercent <= 0.2f) {
                LowHealth.SetActive(true);
                IsLowHealth = true;
            }
        }

        if(IsLowCharge) {
            if(playerCharge.ChargePercentage > 0.2f) {
                LowCharge.SetActive(false);
                IsLowCharge = false;
            }
        } else {
            if(playerCharge.ChargePercentage <= 0.2f) {
                LowCharge.SetActive(true);
                IsLowCharge = true;
            }
        }

        if(IsSuffocating) {
            if(GameManager.instance.PollutionPercentage < playerHealth.CriticalPollution) {
                Suffocating.SetActive(false);
                IsSuffocating = false;
            }
        } else {
            if(GameManager.instance.PollutionPercentage >= playerHealth.CriticalPollution) {
                Suffocating.SetActive(true);
                IsSuffocating = true;
            }
        }


        if(RemainingGameTime <= 0f) {
            GameOver();
        }

        CheckWin();
        if ((lastSpawnInvoked - RemainingGameTime) > 5f) {
            lastSpawnInvoked = RemainingGameTime;
            Invoke("CheckSpawnItems",Random.Range(0,3));
        }
    }

    public void CheckSpawnItems() {
         Debug.Log("checking to spawn");
        if(AllBushes.Length == 0) {
            Debug.Log("No bushes");
            return;
        }
        if(HeartCount < 3 && (lastHeartSpawned - RemainingGameTime) > 30) {
            int toSpawn = Random.Range(1, 3 - HeartCount);
            SpawnItem(Heart, toSpawn);
            HeartCount += toSpawn;
            lastHeartSpawned = RemainingGameTime;
        }
        if(SeedCount < 15 && (lastHeartSpawned - RemainingGameTime) > 10) {
            int toSpawn = Mathf.Clamp(Random.Range(3, 15 - SeedCount),0,15 - SeedCount);
            SpawnItem(Seed, toSpawn);
            SeedCount += toSpawn;
            lasSeedSpawned = RemainingGameTime;
        }

    }

    void SpawnItem(Spawnable item, int Count) {
        // Select bushes that will get spawned item
        for (int i = 0 ; i <= Count; i++) {
            GameObject go = Instantiate(item.obj, AllBushes[Random.Range(0,AllBushes.Length)].transform.position + Vector3.up*item.YOffset,Quaternion.identity);
            Pickable pickable = go.GetComponent<Pickable>();
            pickable.SetNotifier(UsedUp);
            pickable.Count = 2;
        }
    }

    public void UsedUp(string objectName) {
        if(objectName == "Seed") {
            SeedCount--;
        } else if(objectName == "PlayerHealth") {
            HeartCount--;
        }
        Debug.Log(objectName);
    }

    

    void CheckWin() {
        if(GameManager.instance.PollutionPercentage <= 0f) {
            PollutionZero.text="[X]";
            pollutionFixed = true;
        } 
        if (GameManager.instance.PollutionPercentage > 0f && pollutionFixed){
            PollutionZero.text="[ ]";
            pollutionFixed = false;
        }
        bool enemyKilled = false;
        if (isEnemySpawned && Enemy == null) {
            enemyKilled = true;
            KilledEnemy.text ="[X]";
        }

        if(pollutionFixed && enemyKilled) {
            //Todo: load some end credits
            WeWon();
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

    public void WeWon() {
        ResetPollution();
        isEnemySpawned = false;
        pollutionFixed = false;
        RemainingGameTime = TotalGameTime;
        pollutionLevel = MaxPollutionLevel/4f;
        PollutionZero.text="[ ]";
        KilledEnemy.text="[ ]";
        IsLowCharge = false;
        IsLowHealth = false;
        IsLowCharge = false;
        LowHealth.SetActive(false);
        LowCharge.SetActive(false);
        Suffocating.SetActive(false);
        PauseUI.SetActive(false);
        DoPause();

        IsGameEnded = true;
        WeWonScene.SetActive(true);
    }

    public void GameOver() {
        string reason = "";
        if(RemainingGameTime <= 0f) {
            reason = "Ran out of time!";
        } else if(Enemy.GetComponent<EnemyController>().isNearPlayer) {
            reason = "Fried by Voxella.";
        } else {
            reason = "Suffocated by pollution.";
        }
        DeathReason.text = reason;
        ResetPollution();
        isEnemySpawned = false;
        pollutionFixed = false;
        RemainingGameTime = TotalGameTime;
        pollutionLevel = MaxPollutionLevel/4f;
        PollutionZero.text="[ ]";
        KilledEnemy.text="[ ]";
        IsLowCharge = false;
        IsLowHealth = false;
        IsLowCharge = false;
        LowHealth.SetActive(false);
        LowCharge.SetActive(false);
        Suffocating.SetActive(false);

        PauseUI.SetActive(false);
        DoPause();

        IsGameEnded = true;
        GameOverScreen.SetActive(true);
    }

    void CalcPollutionLevel() {
        AllBushes = GameObject.FindGameObjectsWithTag("Vegetation");
        float PollutionPerSec = pollutionBackgroundRate + 
            (GameObject.FindGameObjectsWithTag("Enemy").Length*factoryPollutionMake) - 
            (AllBushes.Length*bushPollutionKill);

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
