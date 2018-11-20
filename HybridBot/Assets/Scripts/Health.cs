using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDeath();
public class Health : MonoBehaviour {
	public float health = 0f;
	public float MaxHealth = 100f;
	bool canTakeDamage = true;

	OnDeath onDeath;


	private void Start() {
		health = MaxHealth;
	}

	public void TakeDamage(float damage) {
		if (!canTakeDamage) {
			return;
		}
		health -= damage;
		if (health <= 0f) {
			canTakeDamage = false;
			if(onDeath != null) {
				onDeath();
			} else {
				DefaultOnDeath();
			}
		}
	}

	public float PercentHealth() {
		return health/MaxHealth;
	}

	public void SetDeathDelegate(OnDeath ondeath) {
		onDeath = ondeath;
	}

	public void DefaultOnDeath() {
		Destroy(gameObject);
	}

}
