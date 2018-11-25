using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void OnPick(int count, float unit);
public class Pickable: MonoBehaviour {
	public int Count = 0;
	public int MaxCount = 0;
	public float Unit = 1f;
	public string Name;

	OnPick onPick;

	GameObject target;
	AudioSource audioSource;

	float maxDistance = 0.01f;

	float speed = 0.5f;
	bool reached = false;

	private void Awake() {
		audioSource = GetComponent<AudioSource>();
		if (audioSource != null) {
			audioSource.loop = false;
			audioSource.enabled = true;
			audioSource.Stop();
		}
		
	}

	public void Pick(GameObject follow, OnPick pickAction) {
		onPick = pickAction;
		target = follow;
		gameObject.tag = "Untagged";
		Collider collider = GetComponent<Collider>();
		if (collider != null) {
			collider.enabled = false;
		}
	}

	private void FixedUpdate() {
		if(PauseMenu.IsPaused) {
			return;
		}
		if (reached) {
			return;
		}
		if(target == null) {
			return;
		}
		if((target.transform.position - transform.position).magnitude > maxDistance) {
			transform.position =  Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
			return;
		}
		// Reached!!
		reached = true;
		if(audioSource != null) {
			audioSource.Play(0);
		}
		// If we should have any animations, add them here.

		// Call onpick
		onPick(Count, Unit);
		
		transform.localScale *= 0.0001f;
		Destroy(gameObject, 5f);
	}
}
