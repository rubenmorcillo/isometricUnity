using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.AI;

public class Sala : MonoBehaviour
{
    public GameObject puntoUnion;
    NavMeshSurface navSur;
    NavMeshSurface[] navMeshSuelo;

    public int anchoInicio;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("iniciando " + gameObject);
        navSur = GetComponent<NavMeshSurface>();
       // navMeshSuelo = GetComponentsInChildren<NavMeshSurface>();
        //updateNavMesh();
        
        
    }

    public void updateNavMesh()
    {
        Debug.Log("redibujando navegación por orden de " + name);
        //foreach (NavMeshSurface surfa in navMeshSuelo)
        //{
        //    surfa.BuildNavMesh();
        //}
        navSur.BuildNavMesh();
        
    }

    public Mesh getGeneralMesh()
    {
        MeshFilter[] meses = gameObject.GetComponentsInChildren<MeshFilter>();
        // Debug.Log("tenemos meshes -> " + meses.Length);
        Mesh finalMesh = new Mesh();

        CombineInstance[] combiners = new CombineInstance[meses.Length];

        for (int i = 0; i < meses.Length; i++)
        {
            combiners[i].subMeshIndex = 0;
            combiners[i].mesh = meses[i].sharedMesh;
            combiners[i].transform = meses[i].transform.localToWorldMatrix;
        }

        finalMesh.CombineMeshes(combiners);

        return finalMesh;
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

   

   
}
