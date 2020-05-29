using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombateManager : MonoBehaviour
{

    public static CombateManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    GameManager gameManager = GameManager.instance;
    List<NPCMove> enemigos;
    List<PlayerMove> unidades;

    GameObject unidadPrueba;
    
    public enum FaseCombate {INICIO, COLOCANDO, COMBATE}
    public FaseCombate fase;

    bool ready;
    void Start()
    {
        Debug.Log("CM: start....");
    }
    public void Combate(Sala sala)
    {
        crearEnemigos(sala);
        fase = FaseCombate.COLOCANDO;

    }

    private void Update()
    {
        if(fase == FaseCombate.COLOCANDO)
        {
            mostrarIniciosDisponibles(LevelManager.salaActiva.GetComponent<Sala>());
            checkMouse();
        }

    }



    private void crearEnemigos(Sala sala)
    {
        System.Random rnd = new System.Random();
        List<Tile> posicionesDisponibles = sala.PuntosInicioEnemigo();
        if (posicionesDisponibles.Count > 0)
        {
            int numeroEnemigos = 0;
            while (numeroEnemigos == 0)
            {
                numeroEnemigos = rnd.Next(posicionesDisponibles.Count);
            }
            Debug.Log("creando " + numeroEnemigos + " enemigos ");
            foreach (GameObject enemigo in sala.dameEnemigos(numeroEnemigos))
            {
                Tile disponible = posicionesDisponibles[0];
                posicionesDisponibles.Remove(disponible);
                Vector3 posicion = disponible.transform.position + new Vector3(0,0.9f,0);
                Instantiate(enemigo, posicion, Quaternion.identity); //la rotación molaría calcularla...
                Debug.Log(enemigo.name);
                //enemigos.Add(enemigo.GetComponent<NPCMove>());
                Debug.Log("voy a colocar " + enemigo + " en " + posicion);
            }
        }
        else
        {
            Debug.Log("en " + sala + " No hay seteadas casillas de spawn de enemigos");
        }
           
    }

    private void mostrarIniciosDisponibles(Sala sala)
    {
        List<Tile> puntosInicio = sala.PuntosInicioPlayer();
        foreach (Tile t in puntosInicio)
        {
            t.selectable = true;
            t.target = false;
        }

    }

    private void checkMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Tile")
            {
                Tile c = hit.collider.GetComponent<Tile>();

                if (c.selectable)
                {
                    c.target = true;
                    if (Input.GetMouseButton(0))
                    {
                        Debug.Log("Colocando tu unidad en " + c);
                        colocarUnidad(c);
                    }
                }
            }
        }
    }
    void colocarUnidad(Tile casilla)
    {
        //qué unidad coloco?
        //Unidad unidad = gameManager.DatosPlayer.coleccionUnidades[0];
        GameObject unidad = new GameObject();
        unidad.name = "UnidadSRC";
       
        GameObject modeloUnidad = (GameObject) Resources.Load("Unidades/"+unidad.name);
        Destroy(unidad);
        Instantiate(modeloUnidad, casilla.transform.position, Quaternion.identity);
        fase = FaseCombate.COMBATE;
    }
}
