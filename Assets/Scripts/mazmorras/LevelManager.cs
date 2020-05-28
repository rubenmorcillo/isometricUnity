
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    GameObject mazmorra;
    GameObject playerAvatar;

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

    }

    public void Start()
    {
        Debug.Log("LevelManager: me inicio");
        //obtengo los datos
       
        //inico los otros managers
        LevelCreator.Init();





        //de momento...
        iniciarMazmorra();
        
    }

    private void iniciarMazmorra()
    {
        mazmorra = LevelCreator.CrearMazmorra();
        LevelCreator.CrearSalaInicial();
        playerAvatar = LevelCreator.posicionarJugador();
        EstadosJuego.activarEstado(EstadosJuego.Estado.EXPLORAR);
    }

    public void abrirPuerta(Puerta puerta)
    {
        LevelCreator.nuevaSala(puerta);
    }

    public void activarCombate()
    {
        EstadosJuego.activarEstado(EstadosJuego.Estado.COMBATE);

        //desactivo a mi avatar
        playerAvatar.SetActive(false);
        //instancio mi muñeco (cuales?)
        GameObject unidad = (GameObject)Resources.Load("UnidadSRC");
        unidad = GameObject.Instantiate(unidad, LevelCreator.salaActiva.transform);
        //
    }

    void RecuperarUnidades()
    {

    }

    void CrearUnidades()
    {

    }



   
}
