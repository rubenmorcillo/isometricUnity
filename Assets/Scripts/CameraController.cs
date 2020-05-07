using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float movementSpeed;
    public float movementTime;
    public float margen = 10f;

    public Vector3 newPosition;

    

    public Vector2 panLimit;
   

    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {

        

        if (newPosition == null)
        {
            newPosition = transform.position;
        }

        if (transform.position.z > 23f)
        {
            newPosition.z = 23f;
        }
        else if (transform.position.z < -2.5f)
        {
            newPosition.z = -2.5f;
        }

        if (Input.mousePosition.y > Screen.height - margen)
        {
            newPosition += transform.forward * movementSpeed;
            //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementSpeed);
        }
        else if(Input.mousePosition.y < margen )
        {
            newPosition += transform.forward * -movementSpeed;
            //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementSpeed);
        }
        
        if(Input.mousePosition.x > Screen.width - margen)
        {
            newPosition += transform.right * movementSpeed;
            //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementSpeed);
        }
        else if(Input.mousePosition.x <=  margen )
        {
            newPosition += transform.right * -movementSpeed;
            //transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementSpeed);
        }



        newPosition.x = Mathf.Clamp(newPosition.x, -panLimit.x, panLimit.x);
        newPosition.z = Mathf.Clamp(newPosition.z, -panLimit.y, panLimit.y);




        transform.position = newPosition;
    }
}
