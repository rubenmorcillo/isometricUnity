using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class LevelCreator : MonoBehaviour
{
    enum orientacionEnum { Arriba, Abajo, Derecha, Izquierda};

    public List<GameObject> prefabs;

    public GameObject player;

    public List<GameObject> unidades;
    List<GameObject> salasCombate = new List<GameObject>();
    List<GameObject> pasillos = new List<GameObject>();
    List<GameObject> salasInicio = new List<GameObject>();

    List<GameObject> salasMazmorra = new List<GameObject>();

    [SerializeField]
    GameObject salaActiva;

    void Start()
    {
        FiltrarPrefabs();
        CrearSalaInicial();
        posicionarJugador();
        
    }

    //// Update is called once per frame
    //void Update()
    //{
       
    //}

    void FiltrarPrefabs()
    {
        foreach(GameObject p in prefabs)
        {
            if (p.name.Contains("Pasillo"))
            {
                pasillos.Add(p);
            }
            else if(p.name.Contains("SalaCombate"))
            {
                salasCombate.Add(p);
            }else if(p.name.Contains("Inicio"))
            {
                salasInicio.Add(p);
            }
        }
    }



    void CrearSalaInicial()
    {
        GameObject sala = Instantiate(salasInicio[0], gameObject.transform);
        sala.transform.Rotate(new Vector3(0, 90, 0));
        salasMazmorra.Add(sala);
       
        salaActiva = sala;
    }

    public void abrirPuerta(Puerta puerta)
    {
        Sala salaActual = salaActiva.GetComponent<Sala>();

        GameObject prefab;
        Sala salaSiguiente;
        //Debug.Log("el tag actual " +salaActual.tag);
        orientacionEnum orientacion = CalcularOrientacion(salaActual.getGeneralMesh(), puerta);
        CrearPasillo(orientacion, puerta);
        ////al principio siempre vamos a crear un pasillo
        //if (salaActual.CompareTag("Respawn"))
        //{
        //    CalcularOrientacion(CombinarMeshes(salaActiva), puerta);
        //   // CalcularOrientacion(salaActiva, puerta.transform.position);
        //   // crearPasillo(puerta.transform.position);
        //}


        //if (!salaActual.CompareTag("Pasillo"))
        //{
        //    //si la sala activa no es un pasillo, tenemos que colocar un pasillo. Porque después de cada Sala hay un pasillo.
        //    Debug.Log(salaActiva.GetComponent<Sala>().puntoUnion.transform);
        //    prefab = Instantiate(pasillos[0], gameObject.transform);
        //    salaSiguiente = prefab.GetComponent<Sala>();
        //    prefab.transform.Translate(salaActual.puntoUnion.transform.position - new Vector3(0, 0, salaSiguiente.anchoInicio + 0.5f));
        //}
        //else
        //{
        //    prefab = salasCombate[new System.Random().Next(salasCombate.Count)];
        //    prefab = Instantiate(prefab, gameObject.transform);
        //    salaSiguiente = prefab.GetComponent<Sala>();
        //    prefab.transform.Translate(salaActual.puntoUnion.transform.position + new Vector3(0, 0, salaSiguiente.anchoInicio + 0.5f));
        //}
        
      
        //prefab.transform.Rotate(new Vector3(0, 90, 0));
        //salaActiva = prefab;

       // Debug.Log("el nuevo tag " + salaSiguiente.tag);
        //tenemos que abrir la puerta y eliminar la anterior sala

    }

    void CrearPasillo( orientacionEnum orientacion, Puerta puerta)
    {
        Debug.Log("La puerta está en " + puerta.transform.position);
        GameObject prefab = pasillos[0]; //sustituir por pasillo random
        prefab = Instantiate(prefab, gameObject.transform);

        Vector3 rotacion = new Vector3();
        Vector3 compensacion = new Vector3();
        Vector3 posicionFinal;
        //a la posicion de la puerta hay que restarle el ancho de la entrada
        Sala salaActual = salaActiva.GetComponent<Sala>();
        Sala pasillo = prefab.GetComponent<Sala>();

        switch (orientacion)
        {
            case orientacionEnum.Abajo:
                rotacion = new Vector3(0,-90, 0);
               // compensacion = new Vector3(-pasillo.anchoInicio,0,0);
                break;
            case orientacionEnum.Arriba:
                rotacion = new Vector3(0, 90, 0);
                compensacion = new Vector3(pasillo.anchoInicio/2, 0, 0);
                //compensacion = new Vector3(salaActual.getGeneralMesh().bounds.extents.x, 0, salaActual.getGeneralMesh().bounds.extents.z);
               // compensacion = salaActual.puntoUnion.transform.position - new Vector3(0, 0, pasillo.anchoInicio + 0.5f);
                break;
            case orientacionEnum.Izquierda:
               // prefab.transform.Rotate(new Vector3(0, 90, 0));
                break;
            case orientacionEnum.Derecha:
                //prefab.transform.Rotate(new Vector3(0, 90, 0));
                break;
            default:
                rotacion = new Vector3(0, 0, 0);
                break;
        }
        //salaActual.getGeneralMesh().bounds.extents
        posicionFinal = puerta.transform.position + compensacion;
        posicionFinal.y = 0;
        prefab.transform.Rotate(rotacion);
        prefab.transform.Translate(posicionFinal);
        

    }

    private orientacionEnum CalcularOrientacion(Mesh meshSala, Puerta puerta)
    {
        Vector3 centro = meshSala.bounds.center;
        Vector3 posicionPuerta = puerta.transform.position;

        float difX = centro.x - posicionPuerta.x;
        float difZ = centro.z + posicionPuerta.z;

        //esto no se si va a funcionar en todos los casos...quizá falle con posiciones muy locas. Probar en mapas grandes
        if (difX < 0)
        {
            difX *= -1;
        }
        if(difZ < 0)
        {
            difZ *= -1;
        }
       
        //buscar una manera de comparar esto absolutamente
        if (difX > difZ)
        {
            if (posicionPuerta.x > centro.x)
            {
                Debug.Log("la puerta está a la derecha");
                return orientacionEnum.Derecha;
            }
            else
            {
                Debug.Log("la puerta está a la izquierda");
                return orientacionEnum.Izquierda;
            }
        }
        else
        {
            if (posicionPuerta.z > centro.z)
            {
                Debug.Log("la puerta está arriba");
                return orientacionEnum.Arriba;
            }
            else
            {
                Debug.Log("la puerta está abajo");
                return orientacionEnum.Abajo;
            }
        }
    }

    
    void posicionarJugador()
    {
        GameObject spawn; 
        spawn = GameObject.Find("playerSpawn");

        if (spawn != null)
        {
          
            ///player.AddComponent<LogicaJugadorMouse>();
            player = Instantiate(player, spawn.transform);
        }
        else
        {
            Debug.Log("error instanciando jugador");
        }
    }

}
