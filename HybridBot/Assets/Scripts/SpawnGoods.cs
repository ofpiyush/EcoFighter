using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnGoods : Spawner {
	public float spawnDelay = 10f;

	private float timer = 0;

	private float notSpawnedSince = 0f;
	private float nextAttemptDelay = 0f;


	private void Start() {
		nextAttemptDelay = spawnDelay;
	}
	private void FixedUpdate() {
		if(PauseMenu.IsPaused) {
			return;
		}

		timer += Time.fixedDeltaTime;
		if(timer > nextAttemptDelay) {
			if (notSpawnedSince > 2*spawnDelay) {
				ForceSpawn(2);
			}

			if(!TrySpawn(1)) {
				notSpawnedSince += timer;
				nextAttemptDelay /= 1.5f;
			} else {
				notSpawnedSince = 0f;
				nextAttemptDelay = spawnDelay;
			}
			timer = 0f;
		}
	}
}
