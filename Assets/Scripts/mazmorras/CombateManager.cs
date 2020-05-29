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
        }else if(fase == FaseCombate.COMBATE)
        {

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
                colocarUnidad(enemigo, disponible);
                Debug.Log(enemigo.GetComponent<NPCMove>());

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
                        GameObject unidadProvisional = (GameObject)Resources.Load("Unidades/UnidadSRC"); //ESTO HAY QUE CAMBIARLO!!!
                        colocarUnidad(unidadProvisional, c);
                        Debug.Log("Colocando " +  unidadProvisional +" en " + c);
                    }
                }
            }
        }
    }
    void colocarUnidad(GameObject modeloUnidad, Tile casilla)
    {
        Instantiate(modeloUnidad, casilla.transform.position + new Vector3(0,0.9f,0), Quaternion.identity);
    }
}
