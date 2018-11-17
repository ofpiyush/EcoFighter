using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPower : MonoBehaviour {

    public float switchAtY = -80f;
    float MaxY = 0f;
    void Start()
    {
        // We should always start at max Y (please)
        MaxY = transform.position.y;
        SetMode();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		SetMode();
	}
	void SetMode()
    {

        if (transform.position.y < switchAtY)
        {
            GameManager.instance.SunMultiplier = 0f;
        }
        else
        {
            GameManager.instance.SunMultiplier = (transform.position.y - switchAtY) / MaxY;
        }
    }
}
