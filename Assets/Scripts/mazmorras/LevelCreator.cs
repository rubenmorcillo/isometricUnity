using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelCreator : MonoBehaviour
{

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

    public void SiguienteSala()
    {
        Sala salaActual = salaActiva.GetComponent<Sala>();
        
        GameObject prefab;
        Sala salaSiguiente;
        Debug.Log("el tag actual " +salaActual.tag);
        if (!salaActual.CompareTag("Pasillo"))
        {
            //si la sala activa no es un pasillo, tenemos que colocar un pasillo. Porque después de cada Sala hay un pasillo.
            Debug.Log(salaActiva.GetComponent<Sala>().puntoUnion.transform);
            prefab = Instantiate(pasillos[0], gameObject.transform);
            salaSiguiente = prefab.GetComponent<Sala>();
            prefab.transform.Translate(salaActual.puntoUnion.transform.position - new Vector3(0, 0, salaSiguiente.anchoInicio + 0.5f));
        }
        else
        {
            Debug.Log("tenemos " + salasCombate.Count + " salas");
            prefab = salasCombate[new System.Random().Next(salasCombate.Count)];
            Debug.Log("Vamos a colocar " + prefab.name);
            prefab = Instantiate(prefab, gameObject.transform);
            salaSiguiente = prefab.GetComponent<Sala>();
            prefab.transform.Translate(salaActual.puntoUnion.transform.position + new Vector3(0, 0, salaSiguiente.anchoInicio + 0.5f));
        }
        //chapuza para colocarlo
        //tenemos que desplazarlo según el ancho de la nuevaSala (y 0.5 más en la X y la Z)
       
        
      
        prefab.transform.Rotate(new Vector3(0, 90, 0));
        salaActiva = prefab;
        Debug.Log("el nuevo tag " + salaSiguiente.tag);
        //tenemos que abrir la puerta y eliminar la anterior sala

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
