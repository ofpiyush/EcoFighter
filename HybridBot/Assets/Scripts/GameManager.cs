using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public float SunMultiplier = 0f;
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
        Cursor.lockState = UnityEngine.CursorLockMode.Confined;
        Cursor.visible = false;
    }
    void SetDay()
    {

    }
}
