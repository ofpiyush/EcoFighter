using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{

    public float RotationSpeed = 10f;
    public float switchAtY = -80f;
    float MaxY = 0f;
    GameManager manager;


    void Start()
    {
        manager = GameManager.instance;
        // We should always start at max Y (please)
        MaxY = transform.position.y;
        SetMode();
    }

    void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, RotationSpeed * Time.deltaTime);
        transform.LookAt(Vector3.zero);
        SetMode();
    }

    void SetMode()
    {

        if (transform.position.y < switchAtY)
        {
            manager.SunMultiplier = 0f;
        }
        else
        {
            manager.SunMultiplier = (transform.position.y - switchAtY) / MaxY;
        }
    }


}
