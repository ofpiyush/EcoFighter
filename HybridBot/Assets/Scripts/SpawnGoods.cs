using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
 public class Spawnable {
    public float probability;
    public GameObject obj;
	public Pickable pickable;
	
 }


public class Spawner : MonoBehaviour {

	public List<Spawnable> Items;
	public bool TrySpawn(int count) {
		int counter = Random.Range(0,Items.Count);
		if(Random.Range(0f,1f) < Items[counter].probability) {

			if(Items[counter].pickable == null) {
				GameObject go = Instantiate(Items[counter].obj,transform.position + Vector3.up*0.05f,Quaternion.identity);
				Items[counter].pickable = go.GetComponent<Pickable>();
				Items[counter].pickable.Count = 0;
			}
			if (Items[counter].pickable.Count < Items[counter].pickable.MaxCount) {
				Items[counter].pickable.Count = Mathf.Clamp(Items[counter].pickable.Count+count,0,Items[counter].pickable.MaxCount);
				return true;
			}
		}
		return false;
	}
}



public class SpawnGoods : Spawner {
	public float spawnDelay = 10f;

	private float timer = 0;

	private void FixedUpdate() {
		if(PauseMenu.IsPaused) {
			return;
		}

		timer += Time.fixedDeltaTime;
		if(timer > spawnDelay && Random.Range(0f,1f) > 0.75f) {
			if (TrySpawn(1)) {
				timer = 0f;
			}
		}
	}
}
