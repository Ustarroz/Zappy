using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public int speed = 5;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
            transform.position = transform.position + Camera.main.transform.forward * speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            transform.position = transform.position - Camera.main.transform.forward * speed * Time.deltaTime;


    }
}
