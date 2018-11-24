using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderModifier : MonoBehaviour {

	Slider bar;

    Image fill;

    public Gradient gradient;

	void Start() {
		bar = GetComponent<Slider>();
		fill = GetComponentInChildren<Image>();
	}

	void FixedUpdate () {
	    fill.color = gradient.Evaluate(bar.value);
	}
}
