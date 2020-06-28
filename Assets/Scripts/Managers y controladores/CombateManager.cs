using Boo.Lang.Runtime.DynamicDispatching;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class CombateManager : MonoBehaviour
{
    public TextMeshProUGUI tmp; //esto hay que quitarlo
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

    GameManager gameManager;
    //List<NPCMove> enemigos;
    //List<PlayerMove> unidades;

    [SerializeField]
    GameObject unidadSeleccionada;
   
    
    public enum FaseCombate {INICIO, COLOCANDO, INICIO_COMBATE, COMBATE, FIN_COMBATE}
    public FaseCombate fase;
    bool playerReady;
    bool ready;
    void Start()
    {
        gameManager = GameManager.instance;
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
            mostrarUnidadesDisponibles();
            checkMouse();
         
            if ((gameManager.DatosPlayer.equipoUnidades.Where(unidad => unidad.isPlaced).Count()) > 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("iniciando combate");
                    playerReady = true;
                }
               
            }
            else
            {
                playerReady = false;
            }
            if (playerReady)
            {
                fase = FaseCombate.INICIO_COMBATE;
            }
        }else if(fase == FaseCombate.INICIO_COMBATE)
        {
            //desactivamos puerta porque el collider nos jode las casillas
            LevelManager.DesactivarPuerta(LevelManager.salaActiva.GetComponentInChildren<Puerta>());
            gameObject.AddComponent<TurnManager>();
            fase = FaseCombate.COMBATE;
        }else if (fase == FaseCombate.FIN_COMBATE)
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
                GameObject nuevoEnemigo  = crearUnidad(enemigo, disponible);
                nuevoEnemigo.GetComponent<NPCMove>().setDatos(new DatosUnidad(0, "enemigo1", 5,20));
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

    private void mostrarUnidadesDisponibles()
    {
        //Debug.Log("tengo ->" + gameManager.DatosPlayer.equipoUnidades.Count + "unidades");
        foreach(DatosUnidad du in gameManager.DatosPlayer.equipoUnidades)
        {
            if (!du.isPlaced)
            {
               // Debug.Log(du.unitName);
            }
        }
    }



    //ESTA FUNCION DEBERÍA ESTAR EN EL CHEQUEADOR DE MOUSE
    private void checkMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //LayerMask layerMaskUI = LayerMask.GetMask("UI");
        if (unidadSeleccionada == null)
        {
            //seleccionar unidad para colocar  => unidadSeleccionada
            IEnumerable<DatosUnidad> unidadesDisponibles =  gameManager.DatosPlayer.equipoUnidades.Where<DatosUnidad>(datos => !datos.isPlaced);
            GameObject modelo = (GameObject)Resources.Load("Unidades/" + unidadesDisponibles.First().modelPrefabName);
            unidadSeleccionada = modelo;
        }
        else
        {
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
                           //cargar la unidad seleccionada

                            
                            GameObject nuevaUnidad = crearUnidad(unidadSeleccionada, c);
                           
                            nuevaUnidad.GetComponent<PlayerMove>().setDatos(gameManager.DatosPlayer.equipoUnidades[0]); //TEMPORAL
                            Debug.Log("Colocando " + unidadSeleccionada + " en " + c);
                            gameManager.DatosPlayer.equipoUnidades[0].isPlaced = true;
                            unidadSeleccionada = null;
                        }
                    }
                }
            }
        }

       
        
    }
    public GameObject crearUnidad(GameObject modeloUnidad, Tile casilla)
    {
       return  Instantiate(modeloUnidad, casilla.transform.position + new Vector3(0,0.9f,0), Quaternion.identity);
    }
    
}
