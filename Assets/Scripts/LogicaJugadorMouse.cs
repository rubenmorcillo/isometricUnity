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
        CheckMousse();
        CheckPuerta();
		
    }

    void CheckMousse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Tile"))
                {
                    motor.MoverAlPunto(hit.point);
                }

            }

        }
    }

    
    public void CheckPuerta()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + new Vector3(0, 1f, 0), Vector3.right, Color.magenta, 10);
        if (Physics.Raycast(transform.position + new Vector3(0,1f,0), Vector3.right,out hit, 1.0f)) {
            if (hit.collider.gameObject.layer == 9 || hit.collider.tag == "Puerta")
            {
                Debug.Log("estás en la salida, brivón");
            }
        }
    }

}
