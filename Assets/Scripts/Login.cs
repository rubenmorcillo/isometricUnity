using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
  

   

    IEnumerator GetRequest(string uri)
    {
        Debug.Log("SM: llamando a " + uri);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {

                Debug.Log("SM: " + pages[page] + ": Error: " + webRequest.error);
                GetRequest(uri);
            }
            else
            {
                Debug.Log("SM: " + pages[page] + ": Exito: " + webRequest.downloadHandler.text);
            }
        }
    }
}
