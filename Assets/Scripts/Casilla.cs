using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla : MonoBehaviour
{
    
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;

    public List<Casilla> adjacencyList = new List<Casilla>();

    ////Needed BFS (breadth first search)
    public bool visited = false;
    public Casilla parent = null;
    public int distance = 0;

    ////For A*
    //public float f = 0;
    //public float g = 0;
    //public float h = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame

    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Reset()
    {
        adjacencyList.Clear();

        current = false;
        target = false;
        selectable = false;

        visited = false;
        parent = null;
        distance = 0;
    }

    //public void FindNeighbors(float jumpHeight, Casilla target)
    public void FindNeighbors(float jumpHeight)
    {
        Reset();

        //CheckTile(Vector3.forward, jumpHeight, target);
        //CheckTile(-Vector3.forward, jumpHeight, target);
        //CheckTile(Vector3.right, jumpHeight, target);
        //CheckTile(-Vector3.right, jumpHeight, target);
        
        CheckTile(Vector3.forward*3f, jumpHeight );
        CheckTile(Vector3.back*3f, jumpHeight);
        CheckTile(Vector3.right*3f, jumpHeight);
        CheckTile(Vector3.left*3f, jumpHeight);
    }

    //public void CheckTile(Vector3 direction, float jumpHeight, Casilla target)
    public void CheckTile(Vector3 direction, float jumpHeight)
    {
        //este vector puede que haya que cambiarlo -> original:  new Vector3(0.25f, (1 + jumpHeight) / 2.0f, 0.25f);
        Vector3 halfExtents = new Vector3(0.25f, (3 + jumpHeight) / 2.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position  + new Vector3(1.5f, 0, 1.5f) + direction, halfExtents);

        Debug.DrawRay(transform.position + new Vector3(1.5f, 0, -1.5f) + Vector3.forward * 3f, halfExtents, Color.red);
        Debug.DrawRay(transform.position + new Vector3(-1.5f, 0, 1.5f) + Vector3.right * 3f, halfExtents, Color.green);
        Debug.DrawRay(transform.position + new Vector3(1.5f, 0, 4.5f) + Vector3.back * 3f, halfExtents, Color.blue);
        Debug.DrawRay(transform.position + new Vector3(4.5f, 0, 1.5f) + Vector3.left * 3f, halfExtents, Color.yellow);
        foreach (Collider item in colliders)
        {
            
            Casilla tile = item.GetComponent<Casilla>();
            if (tile != null && tile.walkable)
            {
                RaycastHit hit;

                //comprueba si hay algo encima de la casilla
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 3))
                {

                    adjacencyList.Add(tile);
                }
            
            }
        }
    }
}
