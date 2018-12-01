using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowTips : MonoBehaviour {

	[TextArea(2,3)]
	public List<string> tips;

	private int current = 0;
	TextMeshProUGUI TipText;
	
	private void Awake() {
		TipText	= GetComponentInChildren<TextMeshProUGUI>();
		TipText.text = "";
	}
	void Start () {
		current = 0;
		ShowTip();
	}

	void ShowTip() {
		if(tips.Count> 0) {
			TipText.text = tips[current];
		}
	}
	public void NextTip() {
		current ++;
		if(current == tips.Count) {
			current = 0;
		}
		ShowTip();
	}
	public void PreviousTip() {
		current--;
		if(current == 0) {
			current = tips.Count-1;
		}
		ShowTip();
	}
	
}
