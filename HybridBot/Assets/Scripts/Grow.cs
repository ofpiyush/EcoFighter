using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour {

	float maxSize = 2.5f;
	public float GrowRate = 0.05f;

	SpawnGoods spawner;

	void Awake() {
		spawner = GetComponent<SpawnGoods>();
		spawner.enabled = false;
	}

	void FixedUpdate () {
		if(PauseMenu.IsPaused) {
			return;
		}
		if(transform.localScale.x < maxSize) {
			transform.localScale = transform.localScale* (1+ ((GrowRate * GameManager.instance.SunMultiplier * Time.deltaTime)/transform.localScale.x));
		} else {
			if(spawner != null) {
				spawner.enabled = true;
			}
			Destroy(this);
		}
	}
}
