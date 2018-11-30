using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDie : Spawner {

	float start;

	float baseProbability = 0.5f;

	Health health;

	void Awake () {
		start = LevelManager.RemainingGameTime;
		health = GetComponent<Health>();
        health.SetDeathDelegate(this.Die);
	}

	void Die() {
		// Increase probability every 10 seconds of life
		if( (start-LevelManager.RemainingGameTime) > 5f) {
			ForceSpawn(Random.Range(1,(int)((start - LevelManager.RemainingGameTime)/10f)));
		}
		// Todo: allow registering animation or something else
		Destroy(gameObject);
	}
}
