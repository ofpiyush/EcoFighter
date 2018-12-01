using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnDeath();
public class Health : MonoBehaviour {
	public float health = 0f;

	public bool isDead = false;
	public float MaxHealth = 100f;

	public float CriticalPollution = 0.8f;

	public float HealthPercent;
	public bool showHealthBar;
	public bool fixedBar;
	public float yOffset = 0.1f;
	public float barScale = 1f;
	bool canMutateHealth = true;
	public float pollutionDamage = 0f;

	OnDeath onDeath;

	public GameObject bar;
	Slider fill;

	private void Start() {
		health = MaxHealth;
		isDead = false;
		HealthPercent = health/MaxHealth;
		if (bar != null) {
			FindAndMakeFill();
			
			fill.value = HealthPercent;
		}
	}

	public void TakeDamage(float damage) {
		AddHealth(-damage);
	}

	public float AddHealth(float delta) {
		if (!canMutateHealth) {
			return delta;
		}
		float returnValue = 0f;
		if ((health+delta) > MaxHealth ) {
			returnValue = health+delta-MaxHealth;
		}
		health = Mathf.Clamp(health+delta,0f,MaxHealth);
		HealthPercent = health/MaxHealth;
		RefreshHealthBar();
		CheckDie();
		return returnValue;
	}

	private void FixedUpdate() {
		if(Gameplay.IsPaused) {
			return;
		}
		if (pollutionDamage > 0f) {
			float damage = pollutionDamage*((2f*GameManager.instance.PollutionPercentage)-0.5f);
			if(GameManager.instance.PollutionPercentage >= CriticalPollution) {
				damage *= 2;
			}
			TakeDamage(damage);
		}
		RepositionBar();
	}

	void RefreshHealthBar() {
		if(!showHealthBar) {
			return;
		}
		if(bar == null) {
			bar = Instantiate(GameManager.instance.HealthBar);
			FindAndMakeFill();
		}
		fill.value = HealthPercent;
	}

	void FindAndMakeFill() {
		fill = bar.GetComponentInChildren<Slider>();
		bar.transform.localScale *= barScale;
	}

	void RepositionBar() {
		if(bar == null || fixedBar) {
			return;
		}
		bar.transform.position = new Vector3(transform.position.x,transform.position.y+yOffset,transform.position.z);
		bar.transform.forward = Camera.main.transform.forward;
	}

	void CheckDie() {
		if (health > 0f) {
			return;
		}
		canMutateHealth = false;
		isDead = true;
		if(onDeath != null) {
			onDeath();
		} else {
			DefaultOnDeath();
		}
		if(bar != null) {
			Destroy(bar);
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
