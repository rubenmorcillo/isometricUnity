using Facebook.Unity.Mobile.IOS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intentoCoruti : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // - After 0 seconds, prints "Starting 0.0 seconds"
        // - After 0 seconds, prints "Coroutine started"
        // - After 2 seconds, prints "Coroutine ended: 2.0 seconds"
      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Debug.Log("pulsando espacio");
            StartCoroutine(corrutina1());
        }else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("pulsando E");
            StartCoroutine(corrutinaConEspera(2.0f));
        }
    }

    private IEnumerator corrutina1()
    {
        Debug.Log("soy la corrutina");
        yield return 0;
        print("He terminao ");
    }

    private IEnumerator corrutinaConEspera(float waitTime)
    {
        Debug.Log("corrutina 2 empezando....espera " + waitTime+" segundos...");
        yield return new WaitForSeconds(waitTime);
        print("He terminao ");
    }
}
