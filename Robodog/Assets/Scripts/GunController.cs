using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public float turnSpeed = 100f;

    void Start()
    {
        //Screen.lockCursor = true;
        Cursor.lockState = UnityEngine.CursorLockMode.Confined;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        turnControl();
    }

    void turnControl()
    {
        float h = turnSpeed * Time.deltaTime * Input.GetAxis("Mouse X");
        transform.Rotate(0, h, 0);
    }
}
