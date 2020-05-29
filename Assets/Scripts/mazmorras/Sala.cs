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

    [SerializeField]
    List<GameObject> posiblesEnemigos;

    public List<Tile> puntosInicioEnemigos;

    public int anchoInicio;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(name +": iniciandome... ");
        navSur = GetComponent<NavMeshSurface>();
       // navMeshSuelo = GetComponentsInChildren<NavMeshSurface>();
        //updateNavMesh();
        
        
    }

    public void updateNavMesh()
    {
        Debug.Log(name +": Redibujando navegación... ");
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

     public List<Tile> PuntosInicioEnemigo()
    {
        List<Tile> lista = new List<Tile>();
        foreach (Tile t in gameObject.GetComponentsInChildren<Tile>())
        {
            if (t.spawnEnemigo)
            {
                lista.Add(t);
            }
        }
        puntosInicioEnemigos = lista;
        return lista;
    }

    public Transform PuntoInicioLibre()
    {
        //para unidades

        //para npcs
        foreach (Tile t in PuntosInicioEnemigo())
        {
            
        }
        return null;
    }
    

    public List<GameObject> dameEnemigos(int n)
    {
        List<GameObject> enemigos = new List<GameObject>();
        System.Random rnd = new System.Random();
        for (int i = 0; i<n; i++)
        {
            enemigos.Add(posiblesEnemigos[rnd.Next(posiblesEnemigos.Count)]);
        }

        return enemigos;
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

   

   
}
