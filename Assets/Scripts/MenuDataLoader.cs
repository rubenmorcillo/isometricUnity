using UnityEngine;
using UnityEngine.UI;

public class MenuDataLoader : MonoBehaviour
{

    public float dinero,rep;
    public string nickname;

    public Text textRep, textDinero, textNickname;
    void Start()
    {
        textDinero = GameObject.Find("jugadorDinero").GetComponent<Text>();
        textRep = GameObject.Find("jugadorRep").GetComponent<Text>();
        textNickname = GameObject.Find("jugadorName").GetComponent<Text>();
    }

    void Update()
    {
        if (GameManager.instance.DatosPlayer != null)
		{
            RefrescarDatosPlayer();
        }
    }

    void RefrescarDatosPlayer()
	{
        textRep.text = "Reputación: ";
        textRep.text = string.Concat(textRep.text, GameManager.instance.DatosPlayer.reputacion);

        textDinero.text = "Dinero: ";
        textDinero.text = string.Concat(textDinero.text, GameManager.instance.DatosPlayer.dinero);

        textNickname.text = GameManager.instance.DatosPlayer.nickname;
    }
}
