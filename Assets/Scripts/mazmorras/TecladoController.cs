using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TecladoController : MonoBehaviour
{
    public GameObject menuEsc;
    // Start is called before the first frame update
    void Start()
    {
        menuEsc = GameObject.Find("menuEsc");
        menuEsc.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckMenuEsc();
    }

    void CheckMenuEsc()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!EstadosJuego.MenuActivo())
            {
                menuEsc.SetActive(true);
                EstadosJuego.setMenuActivo(true);
            }
            else
            {
                menuEsc.SetActive(false);
                EstadosJuego.setMenuActivo(false);
            }
        }
    }
}
