using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOneChild : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int selected = Random.Range(0, transform.childCount);
		int i = 0;
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive(i == selected);
			i++;
		}
		Destroy(this);
	}
	
}
