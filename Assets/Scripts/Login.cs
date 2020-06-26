using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    string serverUri = "https://resealable-compress.000webhostapp.com/test.php";

    [SerializeField]
    TMP_InputField inputLogin, inputPass;

    public void validarLogin()
    {
        Debug.Log("USER: " + inputLogin.text);
        Debug.Log("PASSWORD: " + inputPass.text);
        
        WWWForm formu = new WWWForm();
        formu.AddField("login", inputLogin.text);
        formu.AddField("password", inputPass.text);
        StartCoroutine(testeando(formu));
    }

    IEnumerator testeando(WWWForm form)
    {
        UnityWebRequest request = UnityWebRequest.Post(serverUri, form);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("malamente");
        }
        else
        {
            Debug.Log(request.downloadHandler.text);

            //cojo los datos y relleno datosPlayer
            //falseando los datos
            GameManager.instance.DatosPlayer.nickname = "elNickDelJugador";
            GameManager.instance.DatosPlayer.dinero = 10;
            GameManager.instance.DatosPlayer.reputacion = 99;

            //falseando las unidades q tiene
            DatosUnidad du = new DatosUnidad(1, "rasek", 5, 100);
            DatosUnidad du2 = new DatosUnidad(2, "rusuk", 5, 100);
            du.modelPrefabName = du.unitName;

            GameManager.instance.DatosPlayer.addUnidadEquipo(du);
            GameManager.instance.DatosPlayer.addUnidadEquipo(du2);


            //cuando esté todo cargado, lo llevo a la siguiente escena
            SceneManager.LoadScene("mazmorra_01");

            GameManager.instance.iniciarMazmorra();
          
        }
    }

}
