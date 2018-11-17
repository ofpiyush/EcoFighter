using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public float speedMultiplier = 5f;
    public float fuelChargeRate = 7f;
    public float fuelBurnRate = 5f;
    public float topSpeed = 15f;

    public float maxFuel = 100f;
    float forwardThrust = 0f;
    float sideThrust = 0f;
    public float fuel = 0f;
    public float currentSpeed = 0f;

    Rigidbody rb;
    public Transform racer;

    AudioSource audioData;

    bool isAudioPlaying = false;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        audioData.loop = true;
        fuel = maxFuel / 2f;
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

    void Recharge()
    {
        // If sun isn't shining, don't recharge.
        if (GameManager.instance.SunMultiplier <= 0f)
        {
            return;
        }

        ChangeFuel(GameManager.instance.SunMultiplier * fuelChargeRate * Time.deltaTime);
    }
    void FixedUpdate()
    {

        Recharge();
        currentSpeed = rb.velocity.magnitude;
        Move();
        Jump();
    }

    void Move()
    {
        // Do not add force if out of fuel or at top speed
        if (fuel <= 0f || currentSpeed >= topSpeed)
        {
            return;
        }

        Vector3 fwd = (racer.forward * forwardThrust + racer.right * sideThrust) * Time.deltaTime * speedMultiplier;
        rb.AddForce(new Vector3(fwd.x, 0, fwd.z), ForceMode.VelocityChange);
        if (forwardThrust+sideThrust > 0f)
        {
            ChangeFuel(-fuelBurnRate * Time.deltaTime);
        }
    }

    void Jump()
    {

    }

    void ChangeFuel(float delta)
    {
        fuel = Mathf.Clamp(fuel + delta, 0f, maxFuel);
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


}
