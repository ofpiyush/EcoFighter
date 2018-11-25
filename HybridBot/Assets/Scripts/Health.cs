using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnDeath();
public class Health : MonoBehaviour {
	public float health = 0f;

	public bool isDead = false;
	public float MaxHealth = 100f;
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
		if (bar != null) {
			FindAndMakeFill();
			fill.value = health/MaxHealth;
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
		RefreshHealthBar();
		CheckDie();
		return returnValue;
	}

	private void FixedUpdate() {
		if(PauseMenu.IsPaused) {
			return;
		}
		if (pollutionDamage >0f) {
			TakeDamage(pollutionDamage*((2f*GameManager.instance.PollutionPercentage)-0.5f));
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
		fill.value = health/MaxHealth;
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
