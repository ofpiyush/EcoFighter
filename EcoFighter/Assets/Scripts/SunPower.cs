using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPower : MonoBehaviour {

    public float switchAtY = -80f;
    float MaxY = 0f;
    float MaxIntensity = 0f;
    
    Light sunL;
    private void Awake() {
        sunL = GetComponent<Light>();
        MaxIntensity = sunL.intensity;
       
    }
	private void Start() {
        // We hope that we start at maxY
        MaxY = transform.position.y;
        SetMultiplier();
    }
	// Update is called once per frame
	void FixedUpdate () {
		if(Gameplay.IsPaused) {
			return;
		}

		SetMultiplier();
        sunL.intensity = GameManager.instance.SunMultiplier * MaxIntensity;
	}
	void SetMultiplier()
    {
        GameManager.instance.SunMultiplier = Mathf.Clamp((transform.position.y - switchAtY) / MaxY,0f,1f);
    }
}
