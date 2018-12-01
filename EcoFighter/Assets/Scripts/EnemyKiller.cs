using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKiller : MonoBehaviour {
	EnemyController ctl;

	public float damage = 10f;
	private void Awake() {
		ctl = GetComponentInParent<EnemyController>();
	}
	private void OnTriggerEnter(Collider other) {
		if(Gameplay.IsPaused) {
			return;
		}
		if(other.tag =="Player") {
			ctl.FoundPlayer();
		}
	}

	private void OnTriggerStay(Collider other) {
		if(Gameplay.IsPaused) {
			return;
		}
		if(other.tag == "Vegetation") {
			Health health = other.GetComponent<Health>();
			if(health) {
				health.TakeDamage(damage*Time.deltaTime);
			}
		} else	if(other.tag == "Player") {
			Health health = other.GetComponent<Health>();
			if(health) {
				health.TakeDamage(damage*Time.deltaTime);
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		if(other.tag == "Player") {
			ctl.LostPlayer();
		}
	}

}
