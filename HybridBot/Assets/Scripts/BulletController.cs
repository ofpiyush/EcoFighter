using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public Vector3 target;
	float damage;
	public float speed = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.position =  Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
		if((target-transform.position).magnitude <0.1f) {
			Destroy(gameObject);
		}
	}
}
