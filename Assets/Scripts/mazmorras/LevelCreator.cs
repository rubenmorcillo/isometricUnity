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
       // orientacionEnum orientacion = CalcularOrientacion(salaActual.getGeneralMesh(), puerta);
        
        ////al principio siempre vamos a crear un pasillo
        if (salaActual.CompareTag("Respawn"))
        {
            prefab = pasillos[0]; //sustituir por pasillo random
            CrearPrefab(prefab, puerta);
            
        }else if (salaActual.CompareTag("Pasillo"))
        {
            Debug.Log("estamos en un pasillo");
            prefab = salasCombate[0]; //sustituir por sala random
            CrearPrefab(prefab, puerta);
        }
        else if (salaActual.CompareTag("SalaCombate"))
        {
            prefab = pasillos[0]; //sustituir por pasillo random
            CrearPrefab(prefab, puerta);
        }


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

    void CrearPrefab(GameObject prefab, Puerta puerta)
    {
        //SOLO ESTÁ PROBADO CON PUERTAS CON ORIENTACION NORTE
        Debug.Log("La puerta está en " + puerta.transform.position);
       
        prefab = Instantiate(prefab, gameObject.transform);

        Sala pasillo = prefab.GetComponent<Sala>();
        Quaternion rotacionPuerta = puerta.GetComponentInParent<Transform>().rotation;
        //switch (orientacion)
        //{
        //    case orientacionEnum.Abajo:
        //        rotacion = new Vector3(0,-90, 0);
        //        break;
        //    case orientacionEnum.Arriba:
        //       rotacion = new Vector3(0, 90, 0);
        //        Debug.Log("el centro está en " + salaActual.getGeneralMesh().bounds.center);
        //        break;
        //    case orientacionEnum.Izquierda:
        //       // prefab.transform.Rotate(new Vector3(0, 90, 0));
        //        break;
        //    case orientacionEnum.Derecha:
        //        //prefab.transform.Rotate(new Vector3(0, 90, 0));
        //        break;
        //    default:
        //        rotacion = new Vector3(0, 0, 0);
        //        break;
        //}
        Vector3 posicionFinal = salaActiva.GetComponent<Sala>().puntoUnion.transform.position;
        Debug.Log("la rotacion de la puerta es " + rotacionPuerta);
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
        pasillo.updateNavMesh();
        salaActiva = prefab;
        

    }

    //private orientacionEnum CalcularOrientacion(Mesh meshSala, Puerta puerta)
    //{
    //    Debug.Log("la rotacion es "  +puerta.GetComponentInParent<Transform>().rotation);
    //    Vector3 centro = meshSala.bounds.center;
    //    Vector3 posicionPuerta = puerta.transform.position;

    //    float difX = centro.x - posicionPuerta.x;
    //    float difZ = centro.z + posicionPuerta.z;

    //    //esto no se si va a funcionar en todos los casos...quizá falle con posiciones muy locas. Probar en mapas grandes
    //    if (difX < 0)
    //    {
    //        difX *= -1;
    //    }
    //    if(difZ < 0)
    //    {
    //        difZ *= -1;
    //    }
       
    //    //buscar una manera de comparar esto absolutamente
    //    if (difX > difZ)
    //    {
    //        if (posicionPuerta.x > centro.x)
    //        {
    //            Debug.Log("la puerta está a la derecha");
    //            return orientacionEnum.Derecha;
    //        }
    //        else
    //        {
    //            Debug.Log("la puerta está a la izquierda");
    //            return orientacionEnum.Izquierda;
    //        }
    //    }
    //    else
    //    {
    //        if (posicionPuerta.z > centro.z)
    //        {
    //            Debug.Log("la puerta está arriba");
    //            return orientacionEnum.Arriba;
    //        }
    //        else
    //        {
    //            Debug.Log("la puerta está abajo");
    //            return orientacionEnum.Abajo;
    //        }
    //    }
    //}

    
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
