using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

	public GameObject obj;
	public float delay = 2f;
	private Rigidbody rb;

	private  float startTime;
	private bool instantiated = false;
	private void Awake() {
		rb = GetComponent<Rigidbody>();
		startTime = Time.time;
	}

	void FixedUpdate () {
		if (Time.time - startTime <delay) {
			return;
		}
		if(rb.velocity.magnitude > 0f) {
			return;
		}
		if(instantiated) {
			return;
		}
		Instantiate(obj, transform.position, Quaternion.identity);
		instantiated = true;
		Destroy(gameObject);
	}
}
