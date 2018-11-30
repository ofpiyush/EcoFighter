using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speedMultiplier = 5f;
    public float chargeBurnRate = 5f;
    public float topSpeed = 15f;

    float forwardThrust = 0f;
    float sideThrust = 0f;
    public float currentSpeed = 0f;

    Rigidbody rb;
    public Transform racer;

    //AudioSource audioData;

    bool isAudioPlaying = false;
    Charger charger;
    Health health;

    private void Awake()
    {
        charger = GetComponent<Charger>();
        //audioData = GetComponent<AudioSource>();
        health = GetComponent<Health>();
        if(health != null) {
            health.SetDeathDelegate(this.Die);
        }

        rb = GetComponent<Rigidbody>();
        //audioData.loop = true;
    }
    void Update()
    {
		if(Gameplay.IsPaused) {
			return;
		}

        forwardThrust = Input.GetAxis("Vertical");
        sideThrust = Input.GetAxis("Horizontal");

        // if (currentSpeed > 0f)
        // {
        //     Debug.Log("play sound");
        //     PlayRollingSound();
        // }
        // else
        // {
        //     StopAudio();
        // }

    }


    void FixedUpdate()
    {
		if(PauseMenu.IsPaused) {
			return;
		}

        currentSpeed = rb.velocity.magnitude;
        Move();
        Jump();
    }

    void Move()
    {
        if (health.isDead) {
            return;
        }
        // Do not add force if out of charge or at top speed
        if ((!charger.HasCharge(chargeBurnRate * Time.deltaTime)) || currentSpeed >= topSpeed)
        {
            return;
        }

        Vector3 fwd = (racer.forward * forwardThrust + racer.right * sideThrust);
        fwd = new Vector3(fwd.x,0,fwd.z);
        fwd.Normalize();

        
        rb.AddForce(fwd* Time.deltaTime * speedMultiplier, ForceMode.VelocityChange);
        if (fwd.magnitude > 0f)
        {
            charger.Discharge(chargeBurnRate * Time.deltaTime);
        }
    }

    void Jump()
    {

    }

    // void PlayRollingSound()
    // {
    //     if (!isAudioPlaying)
    //     {
    //         audioData.Play(0);
    //         isAudioPlaying = true;
    //     }

    // }
    // void StopAudio()
    // {
    //     audioData.Stop();
    //     isAudioPlaying = false;
    // }

    void Die() {
        GameManager.instance.GameOver();
    }


}
