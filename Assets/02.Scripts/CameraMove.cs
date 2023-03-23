using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //public Transform target;
    //public float targetY;

    //public float xRotMax;
    //public float rotSpeed;
    //public float scrollSpeed;

    //public float distance;
    //public float minDistance;
    //public float maxDistance;

    //private float xRot;
    //private float yRot;
    //private Vector3 targetPos;
    //private Vector3 dir;

    //private void Update()
    //{
    //    xRot += Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;
    //    yRot += Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
    //    distance += -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

    //    xRot = Mathf.Clamp(xRot, -xRotMax, xRotMax);
    //    distance = Mathf.Clamp(distance, minDistance, maxDistance);

    //    targetPos = target.position + Vector3.up * targetY;

    //    dir = Quaternion.Euler(-xRot, yRot, 0f) * Vector3.forward;
    //    transform.position = targetPos + dir * -distance;
    //}

    //private void LateUpdate()
    //{
    //    transform.LookAt(targetPos);
    //}

    [SerializeField]
    private Transform playerBody;
    [SerializeField]
    private Transform cameraArm;

    private void Update()
    {
        LookAround();
    }


    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }    

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
}
