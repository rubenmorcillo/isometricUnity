﻿using UnityEngine;

[RequireComponent(typeof(TecladoController))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    GameObject mazmorra;
    GameObject playerModel;
    DatosPlayer datosPlayer;

    TecladoController teclado;

    CombateManager combateManager;

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
        DontDestroyOnLoad(this);

        datosPlayer = gameObject.GetComponent<DatosPlayer>();
        combateManager = gameObject.GetComponent<CombateManager>();
        teclado = gameObject.GetComponent<TecladoController>();
        if (combateManager == null)
        {
            combateManager = gameObject.AddComponent<CombateManager>();
        }
        if (teclado == null)
		{
            teclado = gameObject.AddComponent<TecladoController>();
		}
    }

    public DatosPlayer DatosPlayer
    {
        get
        {
            return datosPlayer;
        }
        set
        {
            datosPlayer = value;
        }
       
    }

    //toDo: void CargarDatosPlayer(){}

    public void Start()
    {
        Debug.Log("GameManager: start...");

        teclado = GetComponent<TecladoController>();

        //inico los otros managers
        combateManager.enabled = false;


        //EL LEVEL MANAGER SOLO LO NECESITO CUANDO VOY A LA MAZMORRA
        LevelManager.Init();

    }

    public void iniciarMazmorra()
    {
        Debug.Log("GM: iniciando mazmorra");
        mazmorra = LevelManager.CrearMazmorra();
        LevelManager.CrearSalaInicial();
        playerModel = LevelManager.posicionarJugador();
        EstadosJuego.activarEstado(EstadosJuego.Estado.EXPLORAR);
    }

    public void abrirPuerta(Puerta puerta)
    {
        LevelManager.nuevaSala(puerta);
    }

    public void activarCombate()
    {
        EstadosJuego.activarEstado(EstadosJuego.Estado.COMBATE);
     
        //ToDo:hago la animación que tenga que hacer
        //pongo la musica
        playerModel.SetActive(false); //desactivo a mi avatar
        combateManager.enabled = true;
        combateManager.Combate(LevelManager.salaActiva.GetComponent<Sala>());
        
    }
   
}
