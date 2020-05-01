﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCasillas : MonoBehaviour
{
    List<Casilla> selectableTiles = new List<Casilla>();
    GameObject[] tiles;

    Stack<Casilla> path = new Stack<Casilla>();
    Casilla currentTile;

    public bool moving = false;
    public int moveDistance = 1;
    public float jumpHeight = 2;
    public float moveSpeed = 2;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3(); //la dirección a la que mira

    float halfHeight = 0;

    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Suelo");

        //calculamos el centro del player, con la mitad de la altura desde la casilla
        halfHeight = GetComponent<Collider>().bounds.extents.y;
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public Casilla GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Casilla tile = null;
        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            Debug.DrawRay(target.transform.position, Vector3.up, Color.red);
            tile = hit.collider.GetComponent<Casilla>();
        }
        return tile;
    }

    public void ComputeAdjacencyLists()
    {
        foreach(GameObject tile in tiles)
        {
            Casilla t = tile.GetComponent<Casilla>();
            t.FindNeighbors(jumpHeight);
        }
    }

    public void FindSelectableTiles()
    {
        ComputeAdjacencyLists();
        GetCurrentTile();

        //aqui empezamos el bfs
        Queue<Casilla> process = new Queue<Casilla>();

        process.Enqueue(currentTile);
        currentTile.visited = true;
        //currentTile.parent = ??

        while (process.Count > 0)
        {
            Casilla t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;

            //procesamos todos los tiles que están en nuestra distancia
            if (t.distance < moveDistance)
            {
                foreach (Casilla tile in t.adjacencyList)
                {
                    if (!tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }
            
        }
    }

    public void MoveToTile(Casilla tile)
    {
        Debug.Log(tile.transform.position);
        path.Clear();
        tile.target = true;
        moving = true;

        Casilla next = tile;
        while (next != null)
        {
            Casilla n = next;
            
            path.Push(next);
            next = next.parent;
        }

    }

    public void Move()
    {
        //mover la unidad de un tile al siguiente
        if (path.Count > 0)
        {
           
            Casilla t = path.Peek();
            Debug.Log("YO ESTOY en " +transform.position);
            Debug.Log("...y estoy yendo a -> "+t.transform.position);
            Vector3 target = t.transform.position + new Vector3(1.5f, 0, 1.5f); //esto creo que nos va a mover a la esquina, tendré que calcular el centro +new Vector(1.5,0,1.5);
            //Calculate the unit's position on top of the target tile
            
            //no queremos que se mueva a la altura del tile (porque no hundiría al personaje), lo tenemos que mover a la misma altura 
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;
           // Debug.Log(Vector3.Distance(transform.position, target));
            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                CalculateHeading(target);
                SetHorizontalVelocity();

                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;

            }
            else
            {
                //se supone que hemos llegao
                Debug.Log("hemos llegao");
                transform.position = target;  //a lo mejor también hay que moverlo
                path.Pop();
            }

        }
        else
        {
            RemoveSelectableTiles();
            moving = false;
        }

    }

    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }

        foreach (Casilla tile in selectableTiles)
        {
            tile.Reset();
        }

        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target) {
        heading = target - transform.position;
        Debug.Log("el TARGET está en: " + target);
        Debug.Log("El heading es" + heading);
        heading.Normalize(); //probar a comentar esto si tenemos problemas con el apuntado
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }
}
