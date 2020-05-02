using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCasillas : MonoBehaviour
{
    List<Casilla> selectableTiles = new List<Casilla>();
    GameObject[] tiles;

    Stack<Casilla> path = new Stack<Casilla>();
    Casilla currentTile;

    public bool moving = false;
    public int moveDistance = 3;
    public float jumpHeight = 3;
    public float moveSpeed = 6f;
    public float jumpVelocity = 4.5f;

    float minDistance = 0.35f;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3(); //la dirección a la que mira

    float halfHeight = 0;

    bool fallingDown = false;
    bool jumpingUp = false;
    bool movingEdge = false;
    Vector3 jumpTarget;

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
        //cuando se hace click en una casilla objetivo
        path.Clear();
        tile.target = true;
        moving = true;
       
        Casilla next = tile;



        ////quiero hacer un path correcto
        //RaycastHit hit;
        //Debug.DrawRay(transform.position + new Vector3(0, jumpHeight*1.5f,0), new Vector3(-16.5f,0,7.5f) - transform.position, Color.magenta, 30);
        //if (Physics.Raycast(transform.position + new Vector3(0,jumpHeight*1.5f,0), tile.centerPoint - transform.position, out hit, 3f)){
        //    Debug.Log("chocando");
        //}



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
            
            Vector3 target = t.centerPoint;
            //Calculate the unit's position on top of the target tile

            target.y += halfHeight;
            if (Vector3.Distance(transform.position, target) >= minDistance)
            {
                
                bool jump = transform.position.y != target.y; //DEBUG vigilar esto porque a lo mejor hay que comprobar la diferencia

                if (jump)
                {
                    Jump(target);
                }
                else
                {
                    CalculateHeading(target);
                    SetHorizontalVelocity();
                }
                  
                
                //movimiento
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;

            }
            else
            {
                //Debug.Log("hemos llegao a "+ target);
                transform.position = target; 
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
        heading.Normalize();
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    void Jump(Vector3 target)
    {
       
        if (fallingDown)
        {
            FallDownward(target);
        }
        else if(jumpingUp)
        {
            JumpUpward(target);
        }else if (movingEdge)
        {
            MoveToEdge();
        }
        else
        {
            PrepareJump(target);
        }
    }

    void PrepareJump(Vector3 target)
    {
        float targetY = target.y;
        //aqui arreglamos la Y
        target.y = transform.position.y; //comprobar que pasa si comento ésto

        CalculateHeading(target);

        if (transform.position.y > target.y)
        {
            fallingDown = false;
            jumpingUp = false;
            movingEdge = true;

            jumpTarget = transform.position + (target - transform.position) / 2.0f; //DEBUG comprobar estas posiciones
        }
        else
        {
            fallingDown = false;
            jumpingUp = true;
            movingEdge = false;

            velocity = heading * moveSpeed / 3.0f; //la división es opcional, si va lento hay que variar

            float difference = targetY - transform.position.y;
            //para modificar la altura del salto basta con modificar la jump velocity
            velocity.y = jumpVelocity * (0.5f + difference / 2.0f); 
        }
    }

    void FallDownward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y <= target.y)
        {
            fallingDown = false;

            Vector3 p = transform.position;
            p.y = target.y;
            transform.position = p;

            velocity = new Vector3();
        }
    }

    void JumpUpward(Vector3 target)
    {
       // velocity += (target - transform.position)* Time.deltaTime /2.f; //este es mi potencia de salto personal
        velocity += Physics.gravity * Time.deltaTime;
       
        if (transform.position.y > target.y) //restar jumpHeight para que no suba más del Y de la casilla 
        {
            jumpingUp = false;
            fallingDown = true;

        }
    }

    void MoveToEdge()
    {
        if (Vector3.Distance(transform.position, jumpTarget) >= 0.05f) //DEBUG revisar estas posiciones
        {  

            SetHorizontalVelocity();
        }
        else
        {
            movingEdge = false;
            fallingDown = true;

            
            velocity /= 3.0f; //no veo como influye cambiar esta velocidad
            velocity.y = 1.5f;

        }
    }


}
