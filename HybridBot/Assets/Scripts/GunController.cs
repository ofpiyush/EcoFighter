using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	bool isShooting = false;
    public float damage = 10f;
    public float range = 1.5f;
    public float MaxCharge = 10f;

    public float ThrowForce = 10f;
    public Transform SpawnPoint;
    public GameObject Bomb;
    public float charge = 0f;
    float shotDuration = 0.25f;
    private LineRenderer laserLine;
    private Color originalColor;
    
    private void Awake() {
        charge = MaxCharge;
        laserLine = GetComponent<LineRenderer>();
        if (laserLine != null) {
            laserLine.enabled = false;
        }
        originalColor = laserLine.material.color;
    }

	// Update is called once per frame
	void Update () {
        Recharge();
        if(Input.GetButtonDown("Fire1"))  {
            ShootOne();
        }
        if(Input.GetButtonDown("Fire2"))  {
            ThrowOne();
        }
	}

    void Recharge() {
        if (GameManager.instance.SunMultiplier <= 0f)
        {
            return;
        }

        ChangeFuel(GameManager.instance.SunMultiplier * Time.deltaTime);
    }

    bool ChangeFuel(float delta)
    {
        charge = Mathf.Clamp(charge + delta, 0f, MaxCharge);
        return charge > 0;
    }

    void ThrowOne() {
        float discharge = -5f;
        if (charge + discharge < 0f) {
            return;
        }
        ChangeFuel(discharge);
        GameObject bomb = Instantiate(Bomb, SpawnPoint.position, Quaternion.identity);
        bomb.GetComponent<Rigidbody>().AddForce(transform.forward);
    }

    void ShootOne() {
        float discharge = -(damage/10f);
        if (charge + discharge < 0f) {
            return;
        }

        // Lose 1/10th of damage value
        ChangeFuel(discharge);
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
            if(Physics.Raycast(SpawnPoint.transform.position,transform.forward, out hit, range)) {
                laserLine.SetPosition (1, hit.point);
                if (hit.collider.tag == "Enemy") {
                    laserLine.material.color = Color.red;
                    hit.collider.GetComponent<Health>().TakeDamage(damage * elapsed);
                    //Debug.DrawRay(transform.position, transform.position + (transform.forward * range), Color.red);
                }
            } else {
                laserLine.material.color = originalColor;
                laserLine.SetPosition (1, SpawnPoint.transform.position + (transform.forward * range));
                //Debug.DrawRay(transform.position, transform.position + (transform.forward * range), Color.white);
            }
            yield return null;
            laserLine.enabled = true;
            elapsed = Time.time-start;
        }

        laserLine.enabled = false;
        laserLine.material.color = originalColor;
    }
}
