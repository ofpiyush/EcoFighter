using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{

    public float turnSpeed = 100f;
    float turnAxis;
    float vertAxis;

    float yaw;
    float pitch;


    void Start(){
        Cursor.lockState = UnityEngine.CursorLockMode.Confined;
        if(!GameManager.instance.isIntroScene) {
            Cursor.visible = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
		if(PauseMenu.IsPaused) {
			return;
		}

        turnAxis = Input.GetAxis("Mouse X");
        vertAxis = Input.GetAxis("Mouse Y");
    }

    void FixedUpdate()
    {
		if(PauseMenu.IsPaused) {
			return;
		}

        yaw += turnSpeed * Time.deltaTime * turnAxis;
        pitch = Mathf.Clamp(pitch- turnSpeed * Time.deltaTime * vertAxis, -25f, 25f);
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }
}
