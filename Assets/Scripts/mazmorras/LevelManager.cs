
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

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
        LevelCreator.CrearSalaInicial();
        LevelCreator.posicionarJugador();
        EstadosJuego.activarEstado(EstadosJuego.Estado.EXPLORAR);
    }

    public void abrirPuerta(Puerta puerta)
    {
        LevelCreator.nuevaSala(puerta);
    }

    public void activarCombate()
    {
        //desactivo a mi player
        //instancio mi muñeco (cuales?)

        //
    }

   
}
