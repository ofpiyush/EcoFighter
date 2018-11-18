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
		if(other.tag == "Vegetation") {
			Health health = other.GetComponent<Health>();
			if(health) {
				health.TakeDamage(damage);
			}
		} else	if(other.tag == "Player") {
			ctl.FoundPlayer();
			Health health = other.GetComponent<Health>();
			if(health) {
				health.TakeDamage(damage);
			}
		}
	}

	private void OnTriggerStay(Collider other) {
		
	}

	private void OnTriggerExit(Collider other) {
		if(other.tag == "Player") {
			ctl.LostPlayer();
		}
	}

}
