using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float movementSpeed;
    public float movementTime;

    public Vector3 newPosition;
   

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

        if (Input.mousePosition.y > Screen.height - Screen.height * 0.05)
        {
            newPosition += transform.forward * movementSpeed;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementSpeed);
        }
        else if(Input.mousePosition.y < 0 + Screen.height * 0.05)
        {
            newPosition += transform.forward * -movementSpeed;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementSpeed);
        }
        
        if(Input.mousePosition.x > Screen.width - Screen.width * 0.05)
        {
            newPosition += transform.right * movementSpeed;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementSpeed);
        }
        else if(Input.mousePosition.x <= 0 - Screen.width * 0.05)
        {
            newPosition += transform.right * -movementSpeed;
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementSpeed);
        }

        newPosition = transform.position;
    }
}
