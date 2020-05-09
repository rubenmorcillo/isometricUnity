using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEditor.AI;
using UnityEngine;

public class Sala : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("iniciando " + gameObject);
        NavMeshBuilder.ClearAllNavMeshes();
        NavMeshBuilder.BuildNavMeshAsync();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

   
}
