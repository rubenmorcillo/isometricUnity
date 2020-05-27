using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public static class LevelCreator
{
    enum orientacionEnum { Arriba, Abajo, Derecha, Izquierda};

    static public GameObject player;

    static public List<GameObject> unidades;

    static List<GameObject> salasCombate = new List<GameObject>();
    static List<GameObject> pasillos = new List<GameObject>();
    static List<GameObject> salasInicio = new List<GameObject>();

    static List<GameObject> salasMazmorra = new List<GameObject>();

    static GameObject salaActiva;

    static  NavMeshSurface navegacion;

    static GameObject gameObject;

    public static void Init()
    {
        gameObject = Object.Instantiate(new GameObject("Mapa"));
        gameObject.transform.position = new Vector3(0.5f, 0, 0);
        navegacion = gameObject.AddComponent<NavMeshSurface>();
        player = (GameObject)Resources.Load("testPlayer");
        FiltrarPrefabs();
    }

    public static void SetGameObject(GameObject objeto)
    {
        gameObject = objeto;
    }

    public static void FiltrarPrefabs()
    {

        foreach (GameObject p in Resources.LoadAll("Salas"))
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
        Debug.Log("He filtrado " + salasInicio.Count +" salas de inicio \n" +
            "// filtrado" + salasCombate.Count + " salas de combate \n" +
            "// filtrado " + pasillos.Count + " pasillos");
    }



    public static void CrearSalaInicial()
    {
       // Debug.Log("LC: creando sala inicial");
        System.Random rnd = new System.Random();
        GameObject sala =  Object.Instantiate(salasInicio[rnd.Next(salasInicio.Count)], gameObject.transform);
        //a veces querremos la sala inicial rotada, otras veces no.
        sala.transform.Rotate(new Vector3(0, 90, 0));
        salaActiva = sala;
        navegacion.BuildNavMesh();
        salasMazmorra.Add(sala);
        EstadosJuego.setExplorar(true);
    }

    public static void nuevaSala(Puerta puerta)
    {
        Sala salaActual = salaActiva.GetComponent<Sala>();

        GameObject prefab;
        System.Random rnd = new System.Random();

        //al principio siempre vamos a crear un pasillo
        if (salaActual.CompareTag("Respawn"))
        {
            prefab = pasillos[rnd.Next(pasillos.Count)];
            CrearPrefab(prefab, puerta);
        }
        else if (salaActual.CompareTag("Pasillo"))
        {
            prefab = salasCombate[rnd.Next(salasCombate.Count)];
            CrearPrefab(prefab, puerta);
            ColocarEvento(rnd.Next());
        }
        else
        {
            prefab = pasillos[rnd.Next(pasillos.Count)];
            CrearPrefab(prefab, puerta);
        }
        navegacion.UpdateNavMesh(navegacion.navMeshData);
        salasMazmorra.Add(prefab);
        DesactivarPuerta(puerta);

    }

    static void ColocarEvento(float num)
    {
        num = 0;
        Debug.Log("creando evento con -> " + num);
        if (num > 0.9)
        {
            //salida
        }
        else if (num > 0.6)
        {
            //cofre
        }
        else
        {
            //combate
            //ahora tengo el objeto "evento" en la sala. Hay que crearlo con una posición que esté entre los límites de la sala.

            GameObject evento = GameObject.FindGameObjectWithTag("Evento");
            BoxCollider bc = evento.AddComponent<BoxCollider>();
            float anchoSala = 10; //calcular
            bc.size = new Vector3(anchoSala, 1, 1);
            bc.center = new Vector3(anchoSala/2, 0, 0);
            bc.tag = "Combate";
          
        }
    }

    static void DesactivarPuerta(Puerta puerta)
    {
        puerta.GetComponentInChildren<BoxCollider>().enabled = false ;
    }

    static void CrearPrefab(GameObject prefab, Puerta puerta)
    {
       // Debug.Log("La puerta está en " + puerta.transform.position);
       
        prefab = Object.Instantiate(prefab, gameObject.transform);

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
        prefab.transform.Translate(posicionFinal, gameObject.transform);
        salaActiva = prefab;

    }

   

   
    
    public static void posicionarJugador()
    {
        GameObject spawn; 
        spawn = GameObject.Find("playerSpawn");

        if (spawn != null)
        {
          
            ///player.AddComponent<LogicaJugadorMouse>();
            player = Object.Instantiate(player, gameObject.transform);
            player.transform.Translate(spawn.transform.position);
            EstadosJuego.setIniciado(true);
        }
        else
        {
            Debug.Log("error instanciando jugador // no hay playerSpawn");
        }
    }

}
