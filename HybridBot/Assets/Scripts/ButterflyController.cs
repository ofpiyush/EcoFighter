using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyController : MonoBehaviour
{

    Vector3 target;
    Vector3 initialPosition;

    float maxXdiff = 5f;
    float maxYdiff = 0.75f;
    float maxZdiff = 3f;
    float timer;
    int sec;
    void Start()
    {
        target = ResetTarget();
        sec = ResetSec();

        initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > sec)
        {
            target = ResetTarget();
            sec = ResetSec();
        }
        transform.Translate(target * Time.deltaTime);
    }
    Vector3 ResetTarget()
    {
        return new Vector3(
            DirectedRandom(transform.position.x, initialPosition.x, maxXdiff),
            DirectedRandom(transform.position.y, initialPosition.y, maxYdiff),
            DirectedRandom(transform.position.z, initialPosition.z, maxZdiff)
            );
    }


    float DirectedRandom(float current, float initial, float maxdiff)
    {
        float difference = current - initial;
        if (Mathf.Abs(difference) > maxdiff)
        {
            if (difference > 0f)
            {
                return Random.Range(-2.0f, 0f);
            }
            return Random.Range(0f, 2.0f);
        }
        return Random.Range(-2.0f, 2.0f);
    }
    int ResetSec()
    {
        timer = 0;
        return Random.Range(1, 3);
    }
}
