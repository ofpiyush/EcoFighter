using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
 public class Spawnable {
    public float probability;
    public GameObject obj;
	public Pickable pickable;

	public float YOffset = 0.005f;
	
 }


public class Spawner : MonoBehaviour {

	public List<Spawnable> Items;
	public bool TrySpawn(int count) {
		if (Items.Count == 0) {
			//Report spawned to avoid being called infinitely
			return true;
		}
		int counter = Random.Range(0,Items.Count);
		if(Random.Range(0f,1f) < Items[counter].probability) {
			Spawn(count, counter);
			return true;
		}
		return false;
	}

	public void ForceSpawn(int count) {
		Spawn(count, Random.Range(0, Items.Count));
	}

	void Spawn(int count, int counter) {
		if(Items[counter].pickable == null) {
			GameObject go = Instantiate(Items[counter].obj,transform.position + Vector3.up*Items[counter].YOffset,Quaternion.identity);
			Items[counter].pickable = go.GetComponent<Pickable>();
			Items[counter].pickable.Count = 0;
		}
		Items[counter].pickable.Count = Mathf.Clamp(Items[counter].pickable.Count+count,0,Items[counter].pickable.MaxCount);
	}
}