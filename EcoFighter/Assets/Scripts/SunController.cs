using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{

    public float RotationSpeed = 10f;

    void FixedUpdate()
    {
		if(Gameplay.IsPaused) {
			return;
		}

        transform.RotateAround(Vector3.zero, Vector3.forward, RotationSpeed * Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }
}
