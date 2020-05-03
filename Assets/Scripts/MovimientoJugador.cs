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

       // Debug.DrawRay(transform.position, transform.forward);
        //cuando sea mi turno
        if (!turn)
        {
            return;
        }

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
        //if (Physics.Raycast(ray, out hit))
        //{
        //    if (hit.collider.tag == "Suelo")
        //    {
        //        Casilla c = hit.collider.GetComponent<Casilla>();

        //        if (c.selectable)
        //        {
        //            //toDo: comprobar qué hay en la casilla target
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
