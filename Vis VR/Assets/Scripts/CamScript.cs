using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CamScript : MonoBehaviour
{

    public float speedH;
    public float speedV;

    private float yaw = 0.0f;
    private float pitch = 0.0f;


    void Update()
    {

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

    }

}
