﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject TilePrefab;

    public GameObject Grid;

	// Use this for initialization
	void Start () {
	   
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTileClickedTest()
    {
        Debug.Log("tile clicked");
    }
}