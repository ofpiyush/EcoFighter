using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	float health = 0f;
	public float MaxHealth = 100f;

	private void Start() {
		health = MaxHealth;
	}

	public void TakeDamage(float damage) {
		health -= damage;
		if (health <= 0f) {
			Destroy(gameObject);
		}
	}

	public float PercentHealth() {
		return health/MaxHealth;
	}

}
