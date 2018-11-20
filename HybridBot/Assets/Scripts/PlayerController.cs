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

    AudioSource audioData;

    bool isAudioPlaying = false;
    Charger charger;
    Health health;

    bool canMove = true;
    bool canShoot = true;

    private void Awake()
    {
        charger = GetComponent<Charger>();
        audioData = GetComponent<AudioSource>();
        health = GetComponent<Health>();
        health.SetDeathDelegate(this.Die);
        rb = GetComponent<Rigidbody>();
        audioData.loop = true;
    }
    void Update()
    {
        forwardThrust = Input.GetAxis("Vertical");
        sideThrust = Input.GetAxis("Horizontal");

        if (rb.velocity.magnitude > 3f)
        {
            PlayRollingSound();
        }
        else
        {
            StopAudio();
        }

    }


    void FixedUpdate()
    {
        currentSpeed = rb.velocity.magnitude;
        Move();
        Jump();
    }

    void Move()
    {
        // Do not add force if out of charge or at top speed
        if ((!charger.HasCharge()) || currentSpeed >= topSpeed)
        {
            return;
        }

        Vector3 fwd = (racer.forward * forwardThrust + racer.right * sideThrust) * Time.deltaTime * speedMultiplier;
        rb.AddForce(new Vector3(fwd.x,0f,fwd.z), ForceMode.VelocityChange);
        if (fwd.magnitude > 0f)
        {
            charger.Discharge(chargeBurnRate * Time.deltaTime);
        }
    }

    void Jump()
    {

    }

    void PlayRollingSound()
    {
        if (!isAudioPlaying)
        {
            audioData.Play(0);
            isAudioPlaying = true;
        }

    }
    void StopAudio()
    {
        audioData.Stop();
        isAudioPlaying = false;
    }

    void Die() {
        Debug.Log("Press F to pay respects"+ this.speedMultiplier);
    }


}
