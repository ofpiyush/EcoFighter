using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffects : MonoBehaviour {

	public GameObject effect;
	// Use this for initialization
	void Start () {
		GetComponent<Health>().SetDeathDelegate(Die);
	}

	void Die () {
		Instantiate(effect,transform.position+Vector3.up*0.01f,Quaternion.identity);
		Destroy(gameObject);
	}
}
