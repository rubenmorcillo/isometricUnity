using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    NavMeshSurface navegacion;


    void Start()
    {
        navegacion = gameObject.AddComponent<NavMeshSurface>();
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
            if (p.CompareTag("Pasillo"))
            {
                pasillos.Add(p);
            }
            else if(p.CompareTag("SalaCombate"))
            {
                salasCombate.Add(p);
            }else if(p.CompareTag("Respawn"))
            {
                salasInicio.Add(p);
            }
        }
    }



    void CrearSalaInicial()
    {
        System.Random rnd = new System.Random();
        GameObject sala = Instantiate(salasInicio[rnd.Next(salasInicio.Count)], gameObject.transform);
        //a veces querremos la sala inicial rotada, otras veces no.
        sala.transform.Rotate(new Vector3(0, 90, 0));
        salaActiva = sala;
        navegacion.BuildNavMesh();
        salasMazmorra.Add(sala);
    }

    public void nuevaSala(Puerta puerta)
    {
        Sala salaActual = salaActiva.GetComponent<Sala>();

        GameObject prefab;
        System.Random rnd = new System.Random();
        

        //al principio siempre vamos a crear un pasillo
        //if (salaActual.CompareTag("Respawn"))
        //{
        //   // Debug.Log("hay " + pasillos.Count + " pasillos");
        //    prefab = pasillos[1]; 
        //    CrearPrefab(prefab, puerta);
            
        //}else 

        if (salaActual.CompareTag("Pasillo"))
        {
           // Debug.Log("hay " + salasCombate.Count + " salas");
            prefab = salasCombate[rnd.Next(salasCombate.Count)]; 
           // prefab = salasCombate[1];
            CrearPrefab(prefab, puerta);
        }else
        {
            //Debug.Log("hay " + pasillos.Count + " pasillos");
            prefab = pasillos[rnd.Next(pasillos.Count)];
           // prefab = pasillos[1];
            CrearPrefab(prefab, puerta);
        }
        navegacion.UpdateNavMesh(navegacion.navMeshData);
        salasMazmorra.Add(prefab);
        DesactivarPuerta(puerta);

    }

    void DesactivarPuerta(Puerta puerta)
    {
        puerta.GetComponentInChildren<BoxCollider>().enabled = false ;
    }

    void CrearPrefab(GameObject prefab, Puerta puerta)
    {
        Debug.Log("La puerta está en " + puerta.transform.position);
       
        prefab = Instantiate(prefab, gameObject.transform);

        Sala pasillo = prefab.GetComponent<Sala>();
        Quaternion rotacionPuerta = puerta.GetComponentInParent<Transform>().rotation;
       
        Vector3 posicionFinal = salaActiva.GetComponent<Sala>().puntoUnion.transform.position;
        if (new Quaternion(0, 1.0f, 0, 0).Compare(rotacionPuerta, 2) || new Quaternion(0, -1.0f, 0, 0).Compare(rotacionPuerta, 2))
        {
            Debug.Log("puerta a la derecha");
            posicionFinal -= new Vector3(0.5f, 0, 0.5f);
        }
        else if (new Quaternion(0, 0, 0, 1.0f).Compare(rotacionPuerta, 2) || new Quaternion(0, 0, 0, -1.0f).Compare(rotacionPuerta, 2))
        {
            Debug.Log("puerta a la izquierda");
            posicionFinal += new Vector3(-0.5f, 0, 0.5f);
        }
        else if(!new Quaternion(0, 0.7f, 0, 0.7f).Compare(rotacionPuerta, 2))
        {
            Debug.Log("puerta al sur");
            posicionFinal -= new Vector3(1.0f, 0, 0);
        }

        prefab.transform.rotation = rotacionPuerta;
        prefab.transform.Translate(posicionFinal, transform);
        Debug.Log("me estoy moviendo a  " + posicionFinal);
        salaActiva = prefab;

    }

   

   
    
    void posicionarJugador()
    {
        GameObject spawn; 
        spawn = GameObject.Find("playerSpawn");

        if (spawn != null)
        {
          
            ///player.AddComponent<LogicaJugadorMouse>();
            player = Instantiate(player, gameObject.transform);
            player.transform.Translate(spawn.transform.position);
        }
        else
        {
            Debug.Log("error instanciando jugador");
        }
    }

}
