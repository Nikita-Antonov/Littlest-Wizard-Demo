using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Target for the camera to follow
    public Transform target;

    //Camera Offset
    public Vector3 offset;

    //Speed of the cameras rotation
    public float rotateSpeed;

    //Pivot of the camera for correct rotations
    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;

    void Start()
    {
        offset = target.position - transform.position;

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //X position for mouse and rotation
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);

        //y position for mouse and piovot rotation
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        //Limits of up and down rotation for the camera
        if(pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        //Moving the camera based on the previous imputs and offset
        float yAngle = target.eulerAngles.y;
        float xAngle = pivot.eulerAngles.x;

        Quaternion camRotation = Quaternion.Euler(xAngle, yAngle, 0);
        transform.position = target.position - (camRotation * offset);

        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);
        }

        transform.LookAt(target.position + Vector3.up * 2f);
    }
}
