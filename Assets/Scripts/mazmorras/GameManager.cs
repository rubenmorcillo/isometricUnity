using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    GameObject mazmorra;
    GameObject playerModel;
    DatosPlayer datosPlayer;

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
    }

    public DatosPlayer DatosPlayer
    {
        get
        {
            return datosPlayer;
        }
       
    }

    //toDo: void CargarDatosPlayer(){}

    public void Start()
    {
        Debug.Log("GameManager: start...");

        //inico los otros managers
        combateManager.enabled = false;
        LevelManager.Init();





        //de momento...
        iniciarMazmorra();
        
    }

    private void iniciarMazmorra()
    {
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
        
        //instancio mi muñeco (cuales?)
        //GameObject unidad = (GameObject)Resources.Load("UnidadSRC");
        //unidad = GameObject.Instantiate(unidad, LevelManager.salaActiva.transform);
        
    }
   
}
