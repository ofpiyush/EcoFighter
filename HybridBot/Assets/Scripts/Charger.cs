using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger : MonoBehaviour {


    public float MaxCharge = 10f;

	public float chargeRate = 1f;
	float charge;
	private void Awake() {
		charge = MaxCharge/2f;
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
			charge = 0f;
			return false;
		}
		AddCharge(-delta);
		return true;
	}
    public void AddCharge(float delta)
    {
        charge = Mathf.Clamp(charge + delta, 0f, MaxCharge);
    }

	public bool HasCharge() {
		return charge > 0f;
	}
	public bool HasCharge(float delta) {
		return (charge - delta) >= 0f;
	}
}
