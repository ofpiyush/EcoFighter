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
        if (GameManager.instance.SunMultiplier > 0f)
        {
            fuel += GameManager.instance.SunMultiplier * fuelChargeRate * Time.deltaTime;
            if (fuel > maxFuel)
            {
                fuel = maxFuel;
            }
        }
    }
    void FixedUpdate()
    {

        currentSpeed = rb.velocity.magnitude;
        if (fuel > 0f && currentSpeed < topSpeed)
        {
            Vector3 fwd = (racer.forward * forwardThrust + racer.right * sideThrust * 2f) * Time.deltaTime * speedMultiplier;
            rb.AddForce(new Vector3(fwd.x, 0, fwd.z), ForceMode.VelocityChange);
            if (fwd.magnitude > 0f)
            {
                fuel -= fuelBurnRate * Time.deltaTime;
                if (fuel <= 0f)
                {
                    print("ran out of fuel");
                    fuel = 0f;
                }
            }
        }
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
