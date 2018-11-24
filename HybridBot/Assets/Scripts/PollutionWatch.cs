using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PollutionWatch : MonoBehaviour {
	Slider slider;
	private void Awake() {
		slider = GetComponentInChildren<Slider>();
	}
		
	// Update is called once per frame
	void FixedUpdate () {
		slider.value = GameManager.instance.PollutionPercentage;
	}
}
