using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{

    public List<GameObject> prefabs;

    public GameObject player;

    public List<GameObject> unidades;

    List<GameObject> salasCombate = new List<GameObject>();
    List<GameObject> pasillos = new List<GameObject>();
    List<GameObject> salasInicio = new List<GameObject>();

    List<GameObject> salasMazmorra = new List<GameObject>();
    GameObject salaActiva;
    void Start()
    {
        FiltrarPrefabs();
        CrearSalaInicial();
        posicionarJugador();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
            }else if(p.name == "SalaInicio")
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

    void posicionarJugador()
    {
        GameObject spawn; 
            
        spawn = GameObject.Find("playerSpawn");

        if (spawn != null)
        {
           player = Instantiate(player, spawn.transform);
          // player.AddComponent<MotorJugador>();
           player.AddComponent<LogicaJugadorMouse>();
          
        }
        else
        {
            Debug.Log("error instanciando jugador");
        }

    }




}
