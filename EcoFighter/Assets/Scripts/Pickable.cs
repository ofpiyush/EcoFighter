using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void OnPick(int count, float unit);
public class Pickable: MonoBehaviour {
	public int Count = 0;
	public int MaxCount = 0;
	public float Unit = 1f;
	public string Name;

	public float AutoDestroyAfter = 30f;

	OnPick onPick;

	GameObject target;
	AudioSource audioSource;

	float maxDistance = 0.01f;

	float speed = 0.5f;
	float timer = 0f;
	bool reached = false;

	private void Awake() {
		audioSource = GetComponent<AudioSource>();
		if (audioSource != null) {
			audioSource.loop = false;
			audioSource.enabled = true;
			audioSource.Stop();
		}
		timer = 0f;
	}

	public void Pick(GameObject follow, OnPick pickAction) {
		AutoDestroyAfter = 10000f;
		onPick = pickAction;
		target = follow;
		Disable();
	}

	void Disable() {
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

		timer += Time.deltaTime;
		if (timer > AutoDestroyAfter) {
			Disable();
			transform.localScale *= 0.0001f;
			//Assume reached
			reached = true;
			Destroy(gameObject, 2f);
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
