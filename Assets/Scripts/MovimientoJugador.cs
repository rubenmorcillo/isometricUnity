using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MovimientoCasillas
{


    
    void Start()
    {
        Init();
    }

    void Update()
    {
        //cuando sea mi turno
        if (!moving)
        {
            
            FindSelectableTiles();
            CheckMousePosition();
            CheckMouse();
        }
        else
        {
            Move();
        }
        
    }
    public void CheckMousePosition()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //RaycastHit hit;
        //if(Physics.Raycast(ray, out hit))
        //{
        //    if (hit.collider.tag == "Suelo")
        //    {
        //        Casilla c = hit.collider.GetComponent<Casilla>();

        //        if (c.selectable)
        //        {
        //            c.target = true;
        //        }
        //    }
        //}
    }

    public void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Suelo")
                {
                    Casilla t = hit.collider.GetComponent<Casilla>();
                    if (t.selectable)
                    {
                        //movemos al personaje
                         
                        
                        MoveToTile(t);
                    }
                }
            }
        }
    }

   
}
