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
    [SerializeField]
    List<GameObject> salasCombate = new List<GameObject>();
    [SerializeField]
    List<GameObject> pasillos = new List<GameObject>();
    [SerializeField]
    List<GameObject> salasInicio = new List<GameObject>();

    List<GameObject> salasMazmorra = new List<GameObject>();
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
        if (!pasillos.Contains(salaActiva))
        {
            //si la sala activa no es un pasillo, tenemos que colocar un pasillo. Porque después de cada Sala hay un pasillo.
            int numero = new System.Random().Next(salasCombate.Count-1);
            GameObject newSala = salasCombate[numero];
            Debug.Log("Vamos a colocar " + newSala.name);
        }
    }

    void crearPasillo()
    {

    }

    void posicionarJugador()
    {
        GameObject spawn; 
            
        spawn = GameObject.Find("playerSpawn");

        if (spawn != null)
        {
          
            player.AddComponent<LogicaJugadorMouse>();
            player.AddComponent<NavMeshAgent>();
            player = Instantiate(player, spawn.transform);
        }
        else
        {
            Debug.Log("error instanciando jugador");
        }

    }




}
