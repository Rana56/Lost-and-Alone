using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // the distance the camera will stay from the target
    public float distanceFromTarget = 5; // boolean to toggle following on and off
    public bool follow = false;   //Update the cameras position to follow the target
    
    public float displacementY = 3f;
    public float easing = 0.1f;

    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    //private float yRotation = 0f;

    void Awake(){
        follow = true;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update (){
        if (follow){
            //transform.position = target.position - (target.forward * distanceFromTarget);
            Vector3 newPos = target.position - (Vector3.forward * distanceFromTarget);
            //Vector3 newPos = target.position - (target.forward * distanceFromTarget);

            newPos.y = target.position.y + displacementY;
            //newPos.y = displacementY;

            //Eases camera zoom to 
            transform.position += (newPos - transform.position) * easing;
            //transform.position = newPos;

            //match the rotation of the target so we look directly at it
            //transform.rotation = target.rotation; 
            //transform.LookAt(target.position);

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);

        }
        else {
            transform.LookAt(target);
        }
    }
}
