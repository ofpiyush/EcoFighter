using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;

// [Serializable]
// public class Seeds {

// }

public class GunController : MonoBehaviour {

    public float damage = 10f;
    public float range = 15f;
    public ParticleSystem muzzleFlash;

    public float ThrowForce = 1f;
    int Seeds = 5;
    int MaxSeeds = 50;
    public TextMeshProUGUI SeedCount;
    public Transform SpawnPoint;
    public GameObject Bomb;
    public GameObject Bullet;

    //public List<GameObject> Seeds;

    float shotDuration = 0.25f;
    private LineRenderer laserLine;
    private AudioSource fire;
    private Color originalColor;
    public Charger charger;
    private Camera fpsCam;

    private bool isShooting;
    
    private void Awake() {
        //charger = GetComponent<Charger>();
        laserLine = GetComponent<LineRenderer>();
        fire = GetComponent<AudioSource>();
        if (laserLine != null) {
            laserLine.enabled = false;
        }
        fpsCam = Camera.main;
        originalColor = laserLine.material.color;
        AddSeeds(0);
    }

	// Update is called once per frame
	void Update () {
        if(PauseMenu.IsPaused) {
			return;
		}
        if(Input.GetButtonDown("Fire1"))  {
            if(muzzleFlash == null) {
                ShootOne();
            } else {
                Shoot();
            }

            
        } else {
            if(isShooting) {
                StopShooting();
            }
            isShooting = false;
        }
        if(Input.GetButtonDown("Fire2"))  {
            ThrowOne();
        }
	}

    void Shoot() {
        RaycastHit hit;
         if (!charger.Discharge(damage/10f)) {
            return;
        }
        
        muzzleFlash.Play();
        fire.Play(0);
        

        if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward, out hit, range)) {

            if (hit.collider.tag == "Enemy") {

                hit.collider.GetComponent<Health>().TakeDamage(damage);
                //Debug.DrawRay(transform.position, transform.position + (transform.forward * range), Color.red);
            }
            if (hit.collider.tag == "Vegetation") {

                hit.collider.GetComponent<Health>().TakeDamage(damage);
                //Debug.DrawRay(transform.position, transform.position + (transform.forward * range), Color.red);
            }

        }
    }
    // private void LateUpdate() {
    //     if(isShooting) {
    //         //ShootBullet();
    //         //isShooting = false;
    //         ShootViaUpdate();
    //     }

    // }

    void ThrowOne() {
        if(Seeds == 0 || !charger.Discharge(2f)) {
            return;
        }

        GameObject bomb = Instantiate(Bomb, SpawnPoint.position, Quaternion.identity);
        bomb.GetComponent<Rigidbody>().AddForce(transform.forward*ThrowForce);
        AddSeeds(-1);
    }

    public int AddSeeds(int delta) {
        int returnVal = 0;
        if(Seeds + delta > MaxSeeds) {
            returnVal = Seeds + delta - MaxSeeds;
        }
        
        Seeds = Mathf.Clamp(Seeds+delta, 0, MaxSeeds);

        SeedCount.text = Seeds.ToString("00");
        return returnVal;
    }

    void ShootBullet() {
        GameObject bull = Instantiate(Bullet, SpawnPoint.position, Quaternion.identity);
        BulletController t = bull.GetComponent<BulletController>();
        LineRenderer l = bull.GetComponent<LineRenderer>();
        l.SetPosition(1,fpsCam.transform.forward*0.1f);
        t.target = SpawnPoint.transform.position + (fpsCam.transform.forward * range);
    }

    void StopShooting() {
        isShooting = false;
        laserLine.enabled = false;
        laserLine.material.color = originalColor;
    }
    void ShootViaUpdate() {
        RaycastHit hit;
         if (!charger.Discharge(damage*Time.deltaTime/10f)) {
            laserLine.enabled = false;
            return;
        }
        laserLine.SetPosition (0, SpawnPoint.transform.position);
        if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward, out hit, range)) {
            laserLine.SetPosition (1, hit.point);
            laserLine.enabled = true;
            if (hit.collider.tag == "Enemy") {
                laserLine.material.color = Color.red;
                hit.collider.GetComponent<Health>().TakeDamage(damage*2f * Time.deltaTime);
                //Debug.DrawRay(transform.position, transform.position + (transform.forward * range), Color.red);
            }
            if (hit.collider.tag == "Vegetation") {
                laserLine.material.color = Color.red;
                hit.collider.GetComponent<Health>().TakeDamage(damage * Time.deltaTime);
                //Debug.DrawRay(transform.position, transform.position + (transform.forward * range), Color.red);
            }

        } else {
            laserLine.material.color = originalColor;
            laserLine.SetPosition (1, SpawnPoint.transform.position + (fpsCam.transform.forward * range));
            //Debug.DrawRay(transform.position, transform.position + (transform.forward * range), Color.white);
        }
        laserLine.enabled = true;
    }
    void ShootOne() {
        // Lose 1/10th of damage value
        if (!charger.Discharge(damage/10f)) {
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
                if (hit.collider.tag == "Vegetation") {
                    laserLine.material.color = Color.red;
                    hit.collider.GetComponent<Health>().TakeDamage(damage * elapsed/2f);
                    //Debug.DrawRay(transform.position, transform.position + (transform.forward * range), Color.red);
                }
                // if (hit.collider.tag == "CrystalSource") {
                //     if (!isDoneCalling) {
                //         laserLine.material.color = Color.red;
                //         hit.collider.GetComponent<Spawner>().TrySpawn(3);
                //         isDoneCalling = true;
                //         //Debug.DrawRay(transform.position, transform.position + (transform.forward * range), Color.red);
                //     }
                // }
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
