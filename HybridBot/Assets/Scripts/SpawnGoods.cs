using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
 public class Spawnable {
    public float probability;
    public GameObject obj;
	public Pickable pickable;
	
 }




public class SpawnGoods : MonoBehaviour {

	public List<Spawnable> Items;

	public float spawnDelay = 10f;

	private float timer = 0;

	private void FixedUpdate() {
		timer += Time.fixedDeltaTime;
		if(timer > spawnDelay && Random.Range(0f,1f) > 0.75f) {
			int counter = Random.Range(0,Items.Count);
			if(Random.Range(0f,1f) < Items[counter].probability) {
				timer = 0f;
				if(Items[counter].pickable == null) {
					GameObject go = Instantiate(Items[counter].obj,transform.position + Vector3.up*0.08f,Quaternion.identity);
					Items[counter].pickable = go.GetComponent<Pickable>();
					Items[counter].pickable.Count = 0;
				}
				Items[counter].pickable.Count += 1;
			}
		}
	}
}
