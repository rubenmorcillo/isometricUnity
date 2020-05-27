
using UnityEngine;

public static class LevelManager
{
    // Start is called before the first frame update


   
    public static void Init()
    {
        Debug.Log("LevelManager: me inicio");
        LevelCreator.CrearSalaInicial();
        LevelCreator.posicionarJugador();
        
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public static void abrirPuerta(Puerta puerta)
    {
        LevelCreator.nuevaSala(puerta);
    }

   
}
