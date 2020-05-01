using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuScript
{
    
    
    //[MenuItem("Tools/AssignTile Material")]
    //public static void AssignTileMaterial()
    //{
    //    GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
    //    Material material = Resources.Load<Material>("Tile");

    //    foreach (GameObject t in tiles)
    //    {
    //        t.GetComponent<Renderer>().material = material;
    //    }
    //}

    [MenuItem("Tools/AssignTile Script")]
    public static void AssignTileScript()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Suelo");

        foreach (GameObject t in tiles)
        {

            t.AddComponent<Casilla>();
        }
    }


}
