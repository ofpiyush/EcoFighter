﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject HealthBar;
    public static GameManager instance;
    public float SunMultiplier = 0f;

    public float pollutionLevel = 0f;

    public float pollutionBackgroundRate = 1.5f;

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

    void SetDay()
    {

    }
}
