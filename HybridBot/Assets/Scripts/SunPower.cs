using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunPower : MonoBehaviour {

    public float switchAtY = -80f;
    float MaxY = 0f;
    float MaxIntensity = 0f;
    
    Light light;
    private void Awake() {
        light = GetComponent<Light>();
        // We hope that we start at maxY
        
        MaxIntensity = light.intensity;
       
    }
	private void Start() {
        MaxY = transform.position.y;
        SetMultiplier();
    }
	// Update is called once per frame
	void FixedUpdate () {
		SetMultiplier();
        light.intensity = GameManager.instance.SunMultiplier * MaxIntensity;
	}
	void SetMultiplier()
    {
        GameManager.instance.SunMultiplier = Mathf.Clamp((transform.position.y - switchAtY) / MaxY,0f,1f);
    }
}
