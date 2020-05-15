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
    LevelCreator levelCreator;


    void Start()
    {
        cam = Camera.main;
        if (motor == null)
        {
            motor = gameObject.AddComponent<MotorJugador>();
        }
        else
        {
            motor = GetComponent<MotorJugador>();
        }
        
        levelCreator = FindObjectOfType<LevelCreator>();
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
                if (!hit.collider.CompareTag("Muro"))
                {
                    motor.MoverAlPunto(hit.point);
                }

            }

        }
    }

    
    public void CheckPuerta()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down, Color.magenta, 10);
        if (Physics.Raycast(transform.position + new Vector3(0,1,0), Vector3.down,out hit, 1.0f,9)) {
            if (hit.collider.gameObject.layer == 9 || hit.collider.tag == "Puerta")
            {
                Debug.Log("pulsa espacio para continuar");
                if (Input.GetKey(KeyCode.Space))
                {
                    levelCreator.SiguienteSala();
                }

              
            }
          

        }
       
    }

}
