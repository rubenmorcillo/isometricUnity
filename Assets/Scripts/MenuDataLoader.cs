using Boo.Lang;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuDataLoader : MonoBehaviour
{

    public float dinero,rep;
    public string nickname;
    public Image[] imgUnidades = new Image[5];

    public Text textRep, textDinero, textNickname;
    DatosPlayer datosPlayer;


    void Start()
    {
        textDinero = GameObject.Find("jugadorDinero").GetComponent<Text>();
        textRep = GameObject.Find("jugadorRep").GetComponent<Text>();
        textNickname = GameObject.Find("jugadorName").GetComponent<Text>();
        imgUnidades = GameObject.Find("jugadorUnidades").GetComponentsInChildren<Image>();
    }

    void Update()
    {
        datosPlayer = GameManager.instance.DatosPlayer;
        if (datosPlayer != null)
		{
            RefrescarDatosPlayer();
        }
    }

    void RefrescarDatosPlayer()
	{
        

        textRep.text = "Reputación: ";
        textRep.text = string.Concat(textRep.text, datosPlayer.reputacion);

        textDinero.text = "Dinero: ";
        textDinero.text = string.Concat(textDinero.text, datosPlayer.dinero);

        textNickname.text = datosPlayer.nickname;
    
        for (int i=0; i< datosPlayer.equipoUnidades.Count; i++)
		{
            
            string nombre = datosPlayer.equipoUnidades[i].unitName;
            Sprite img = Resources.Load<Sprite>("Kaos/" + nombre);
            Debug.Log("Estoy cambiando " + imgUnidades[i] + " por -> " + nombre);
            imgUnidades[i].sprite = img;


        }
  //      foreach(DatosUnidad datosUnidad in datosPlayer.equipoUnidades)
		//{
  //          Debug.Log("unidad: " + datosUnidad.unitName);
		//}


    }
}
