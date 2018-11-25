using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDie : Spawner {

	float start;

	float baseProbability = 0.5f;

	Health health;

	void Awake () {
		start = Time.time;
		health = GetComponent<Health>();
        health.SetDeathDelegate(this.Die);
	}

	void Die() {
		// Increase probability every 10 seconds of life
		float Multiplier = (Time.time - start) / 10f;
		Debug.Log(Multiplier);
		if(Multiplier*baseProbability > Random.Range(0f,1f)) {
			TrySpawn(Random.Range(1,(int)Multiplier));
		}
		// Todo: allow registering animation or something else
		Destroy(gameObject);
	}
}
