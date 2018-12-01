using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour {

	float maxSize = 2.5f;
	public float GrowRate = 0.05f;


	void FixedUpdate () {
		if(Gameplay.IsPaused) {
			return;
		}
		if(transform.localScale.x < maxSize) {
			transform.localScale = transform.localScale* (1+ ((GrowRate * GameManager.instance.SunMultiplier * Time.deltaTime)/transform.localScale.x));
		} else {
			Destroy(this);
		}
	}
}
