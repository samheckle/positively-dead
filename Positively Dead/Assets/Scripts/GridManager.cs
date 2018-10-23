using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    public GameObject player;
    private GameObject[] activeGrid;

	// Use this for initialization
	void Start () {
        activeGrid = GameObject.FindGameObjectsWithTag("Tile");
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
