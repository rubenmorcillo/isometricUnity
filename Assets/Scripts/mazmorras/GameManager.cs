using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    GameObject mazmorra;
    GameObject playerModel;
    DatosPlayer datosPlayer;

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
    }

    //toDo: void CargarDatosPlayer(){}

    public void Start()
    {
        Debug.Log("LevelManager: me inicio");
       
        //inico los otros managers
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

        playerModel.SetActive(false); //desactivo a mi avatar
        //colocarEnemigos();



        //instancio mi muñeco (cuales?)
        GameObject unidad = (GameObject)Resources.Load("UnidadSRC");
        unidad = GameObject.Instantiate(unidad, LevelManager.salaActiva.transform);
        
    }

    void RecuperarUnidades()
    {

    }

    void CrearUnidades()
    {

    }



   
}
