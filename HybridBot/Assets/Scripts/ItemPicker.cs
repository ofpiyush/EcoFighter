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
		if (pickable.Name == "Seed") {
			int remaining = gunController.AddSeeds(pickable.Count);
			pickable.Count = remaining;
		} else if(pickable.Name == "GunCharge") {
			float power = pickable.Unit * pickable.Count;

			float remaining = gunController.charger.AddCharge(power);
			// I hope C sharp doesn't have weird bugs else we won't get much power
			pickable.Count = (int)(remaining/pickable.Unit);
		} else if(pickable.Name == "PlayerCharge") {
			float power = pickable.Unit * pickable.Count;

			float remaining = PlayerCharger.AddCharge(power);
			// I hope C sharp doesn't have weird bugs else we won't get much power
			pickable.Count = (int)(remaining/pickable.Unit);
		} else if(pickable.Name == "PlayerHealth") {
			float health = pickable.Unit * pickable.Count;

			float remaining = PlayerHealth.AddHealth(health);
			// I hope C sharp doesn't have weird bugs else we won't get much power
			pickable.Count = (int)(remaining/pickable.Unit);
		}
		pickable.DonePicking();
	}
}
