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
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("CM: start....");
    }
    public void Combate(Sala sala)
    {
        crearEnemigos(sala);
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
                Vector3 posicion = disponible.transform.position;
                Instantiate(enemigo, posicion, Quaternion.identity); //la rotación molaría calcularla...
                Debug.Log("voy a colocar " + enemigo + " en " + posicion);
            }
        }
        else
        {
            Debug.Log("en " + sala + " No hay seteadas casillas de spawn de enemigos");
        }
           
    }
}
