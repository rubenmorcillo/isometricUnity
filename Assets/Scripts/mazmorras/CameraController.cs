using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraController : MonoBehaviour
{
    public float movementSpeed;
    public float movementTime;
    public float margen = 10f;

    public Vector3 newPosition;


    public Vector2 panLimit = new Vector2(10,10);
    GameObject target;

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    private void Start()
    {
        //esto lo tendría que hacer en el SceneManager
        //lo hago aqui porque no tengo otro sitio de momento
        Debug.Log("iniciando el manager");
        LevelCreator.Init();
        LevelManager.Init();
    }

    void Update()
    {
        if (EstadosJuego.Iniciado())
        {
            HandleMovementInput();
        }

    }

    void HandleMovementInput()
    {


        if (newPosition == null)
        {
            newPosition = transform.position;
        }

       

        if (Input.mousePosition.y > Screen.height - margen)
        {
            newPosition += transform.forward * movementSpeed;
        }
        else if(Input.mousePosition.y < margen )
        {
            newPosition += transform.forward * -movementSpeed;
        }
        
        if(Input.mousePosition.x > Screen.width - margen)
        {
            newPosition += transform.right * movementSpeed;
        }
        else if(Input.mousePosition.x <=  margen )
        {
            newPosition += transform.right * -movementSpeed;
        }


        newPosition.x = Mathf.Clamp(newPosition.x, target.transform.position.x - panLimit.x, target.transform.position.x + panLimit.x);
        newPosition.z = Mathf.Clamp(newPosition.z, target.transform.position.z - panLimit.y, target.transform.position.z + panLimit.y);


        transform.position = newPosition;
    }
}
