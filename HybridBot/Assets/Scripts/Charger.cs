using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charger : MonoBehaviour {


    public float MaxCharge = 10f;

	public float chargeRate = 1f;

	public float initialCharge = 10f;
	float charge;

	public Slider fill;
	private void Awake() {
		charge = initialCharge;
	}

	private void Start() {
		DrawFill();
	}
	void FixedUpdate () {
		Recharge();
	}
 	void Recharge() {
        if (GameManager.instance.SunMultiplier <= 0f)
        {
            return;
        }
        AddCharge(GameManager.instance.SunMultiplier * chargeRate * Time.deltaTime);
    }
	
	public bool Discharge(float delta) {
		if (!HasCharge(delta)) {
			// Zero out the charge if we can't discharge the minimum requirement
			//charge = 0f;
			return false;
		}
		AddCharge(-delta);
		return true;
	}
    public float AddCharge(float delta)
    {
		float returnValue = 0f;
		if ((charge + delta) > MaxCharge) {
			returnValue = charge + delta - MaxCharge;
		}
        charge = Mathf.Clamp(charge + delta, 0f, MaxCharge);
		DrawFill();
		return returnValue;
    }

	void DrawFill() {
		float ChargePercentage = charge/MaxCharge;

		if(fill != null) {
			fill.value = ChargePercentage;
		}
	}

	public bool HasCharge() {
		return charge > 0f;
	}
	public bool HasCharge(float delta) {
		return (charge - delta) >= 0f;
	}
}
