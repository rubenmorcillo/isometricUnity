using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEditor.AI;
using UnityEngine;

public class Sala : MonoBehaviour
{
    public GameObject puntoUnion;
    public int anchoInicio;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("iniciando " + gameObject);
        updateNavMesh();
        
        
    }

    public void updateNavMesh()
    {
        Debug.Log("redibujando navegación por orden de " + name);
        NavMeshBuilder.ClearAllNavMeshes();
        NavMeshBuilder.BuildNavMeshAsync();
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
