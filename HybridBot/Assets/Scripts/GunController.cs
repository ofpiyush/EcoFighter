using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public float damage = 10f;
    public float range = 1.5f;

    public float ThrowForce = 1f;
    public Transform SpawnPoint;
    public GameObject Bomb;
    float shotDuration = 0.25f;
    private LineRenderer laserLine;
    private Color originalColor;
    private Charger charger;
    private Camera fpsCam;
    
    private void Awake() {
        charger = GetComponent<Charger>();
        laserLine = GetComponent<LineRenderer>();
        if (laserLine != null) {
            laserLine.enabled = false;
        }
        fpsCam = Camera.main;
        originalColor = laserLine.material.color;
    }

	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Fire1"))  {
            ShootOne();
        }
        if(Input.GetButtonDown("Fire2"))  {
            ThrowOne();
        }
	}

   

    void ThrowOne() {
        if (!charger.Discharge(5f)) {
            return;
        }

        GameObject bomb = Instantiate(Bomb, SpawnPoint.position, Quaternion.identity);
        bomb.GetComponent<Rigidbody>().AddForce(transform.forward*ThrowForce);
    }

    void ShootOne() {
        // Lose 1/10th of damage value
        if (!charger.Discharge( damage/10f)) {
            return;
        }

        StopCoroutine(ShotEffect());
        StartCoroutine(ShotEffect());
    }



    private IEnumerator ShotEffect() {
        
        //yield return shotDuration;
        float start = Time.time;
        float elapsed = 0;
        while(elapsed < shotDuration) {
            RaycastHit hit;
            laserLine.SetPosition (0, SpawnPoint.transform.position);
            if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward, out hit, range)) {
                laserLine.SetPosition (1, hit.point);
                if (hit.collider.tag == "Enemy") {
                    laserLine.material.color = Color.red;
                    hit.collider.GetComponent<Health>().TakeDamage(damage * elapsed);
                    //Debug.DrawRay(transform.position, transform.position + (transform.forward * range), Color.red);
                }
            } else {
                laserLine.material.color = originalColor;
                laserLine.SetPosition (1, SpawnPoint.transform.position + (fpsCam.transform.forward * range));
                //Debug.DrawRay(transform.position, transform.position + (transform.forward * range), Color.white);
            }
            yield return 0.1;
            laserLine.enabled = true;
            elapsed = Time.time-start;
        }

        laserLine.enabled = false;
        laserLine.material.color = originalColor;
    }
}
