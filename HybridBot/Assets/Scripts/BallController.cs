using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public float speedMultiplier = 5f;
    public float topSpeed = 250f;
    float forwardThrust = 0f;
    float sideThrust = 0f;

    Rigidbody rb;
    public Transform racer;

    AudioSource audioData;

    bool isAudioPlaying = false;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        audioData.loop = true;
    }
    void Update()
    {
        forwardThrust = Input.GetAxis("Vertical");
        sideThrust = Input.GetAxis("Horizontal");

        if (rb.velocity.magnitude > 1f)
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

        if (GameManager.instance.SunMultiplier > 0f && rb.velocity.magnitude < topSpeed)
        {
            Vector3 fwd = GameManager.instance.SunMultiplier * (racer.forward * forwardThrust + racer.right * sideThrust * 2f) * Time.deltaTime * speedMultiplier;
            rb.AddForce(new Vector3(fwd.x, 0, fwd.z), ForceMode.VelocityChange);
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
