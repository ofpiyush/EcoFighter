using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour {


	public Health PlayerHealth;
	public Charger PlayerCharger;
	public GunController gunController;

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag != "PickableItem") {
			return;
		}

		Pickable pickable = other.GetComponent<Pickable>();
		if (pickable == null) {
			return;
		}
		bool isPicked = false;
		if (pickable.Name == "Seed") {
			pickable.Pick(gameObject, SeedPicked);
		} else if(pickable.Name == "GunCharge" || pickable.Name == "Crystal") {
			pickable.Pick(gameObject, GunRecharge);
		} else if(pickable.Name == "PlayerCharge") {
			pickable.Pick(gameObject, PlayerRecharge);
		} else if(pickable.Name == "PlayerHealth") {
			pickable.Pick(gameObject, HealPlayer);
		}
	}

	void SeedPicked(int count, float unit) {
		if(gunController == null) {
			return;
		}
		gunController.AddSeeds(count);
	}

	void GunRecharge(int count, float unit) {
		if(gunController == null) {
			return;
		}
		gunController.charger.AddCharge(unit * count);
	}
	void PlayerRecharge(int count, float unit) {
		if(PlayerCharger == null) {
			return;
		}
		PlayerCharger.AddCharge(unit * count);
	}

	void HealPlayer(int count, float unit) {
		if(PlayerHealth == null) {
			return;
		}
		PlayerHealth.AddHealth(unit * count);
	}


}
