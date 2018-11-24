using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable: MonoBehaviour {
	public int Count = 0;
	public float Unit = 1f;
	public string Name;

	public void DonePicking() {
		if ( Count <= 0 ) {
			Destroy(gameObject);
		}
	}
}
