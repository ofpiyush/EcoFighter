using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnGoods : Spawner {
	public float spawnDelay = 10f;

	private float timer = 0;

	private void FixedUpdate() {
		if(PauseMenu.IsPaused) {
			return;
		}

		timer += Time.fixedDeltaTime;
		if(timer > spawnDelay && Random.Range(0f,1f) > 0.75f) {
			if (TrySpawn(1)) {
				timer = 0f;
			}
		}
	}
}
