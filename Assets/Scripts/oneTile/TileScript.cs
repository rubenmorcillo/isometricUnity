using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileScript : MonoBehaviour {

	public bool walkable = true;
	public bool current = false;
	public bool target = false;
	public bool selectable = false;

	public List<TileScript> adjacencyList = new List<TileScript>();

	//Needed BFS ( breadth first search)
	public bool visited = false;
	public TileScript parent = null;
	public int distance = 0;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		if (current) {
			GetComponent<Renderer> ().material.color = Color.magenta;
		} else if (target) {
			GetComponent<Renderer> ().material.color = Color.green;
		} else if (selectable) {
			GetComponent<Renderer> ().material.color = Color.red;
		} else {
			GetComponent<Renderer> ().material.color = Color.white;
		}
	}

	public void Reset(){
		adjacencyList.Clear ();
		Debug.Log ("reset TS");

		walkable = true;
		current = false;
		target = false;
		selectable = false;

		visited = false;
		parent = null;
		distance = 0;
	}
	public void FindNeighbors(float jumpHeight){
		Reset ();
		CheckTile (Vector3.forward, jumpHeight);
		CheckTile (-Vector3.forward, jumpHeight);

		CheckTile (Vector3.right, jumpHeight);
		CheckTile (-Vector3.right, jumpHeight);
	}

	public void CheckTile(Vector3 direction, float jumpHeight){

		Vector3 halfExtents = new Vector3 (0.25f,(1 * jumpHeight) / 2.0f, 0.25f);
		Collider[] colliders = Physics.OverlapBox (transform.position + direction, halfExtents);
		foreach (Collider item in colliders) {
			TileScript tile = item.GetComponent<TileScript> ();
			if (tile != null && tile.walkable) {
				RaycastHit hit;

				if (Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1)){
					adjacencyList.Add (tile);
				
				}
			}
		}
	}
}
