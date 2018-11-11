using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    public float turnSpeed = 100f;
    float turnAxis;

    void Start()
    {
        //Screen.lockCursor = true;
        Cursor.lockState = UnityEngine.CursorLockMode.Confined;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        turnAxis = Input.GetAxis("Mouse X");
    }

    void FixedUpdate()
    {
        float h = turnSpeed * Time.deltaTime * turnAxis;
        transform.Rotate(new Vector3(0, h, 0));
    }
}
