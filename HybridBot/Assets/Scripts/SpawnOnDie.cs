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
		if( (LevelManager.RemainingGameTime - start) > 5f) {
			ForceSpawn(Random.Range(1,(int)((LevelManager.RemainingGameTime - start)/10f)));
		}
		// Todo: allow registering animation or something else
		Destroy(gameObject);
	}
}
