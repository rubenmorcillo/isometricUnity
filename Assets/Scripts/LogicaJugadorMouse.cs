using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MotorJugador))]

public class LogicaJugadorMouse : MonoBehaviour
{
public LayerMask mascMov;

    Camera cam;
	public GameObject camPos;
    MotorJugador motor;


    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<MotorJugador>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit)){
                if(hit.collider.CompareTag("Suelo")){
					  motor.MoverAlPunto(hit.point);
				}

            }
            
        }
		
		
    }
}
