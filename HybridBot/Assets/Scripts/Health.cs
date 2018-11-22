using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnDeath();
public class Health : MonoBehaviour {
	public float health = 0f;
	public float MaxHealth = 100f;
	public bool showHealthBar;
	public bool fixedBar;
	public float yOffset = 0.1f;
	public float barScale = 1f;
	bool canTakeDamage = true;

	OnDeath onDeath;

	public GameObject bar;
	Slider fill;

	private void Start() {
		health = MaxHealth;
		if (bar != null) {
			FindAndMakeFill();
			fill.value = health/MaxHealth;
		}
	}

	public void TakeDamage(float damage) {
		if (!canTakeDamage) {
			return;
		}
		health = Mathf.Clamp(health-damage,0f,MaxHealth);
		RefreshHealthBar();
		CheckDie();

	}

	private void FixedUpdate() {
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
		RepositionBar();

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
		canTakeDamage = false;
		if(onDeath != null) {
			onDeath();
		} else {
			DefaultOnDeath();
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
		if(bar != null) {
			Destroy(bar);
		}

	}

}
