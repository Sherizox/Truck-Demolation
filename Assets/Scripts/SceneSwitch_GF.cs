﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitch_GF : MonoBehaviour {
	public int time;
	public int scene;
	// Use this for initialization
	void Start () {
        FindObjectOfType<bl_SceneLoader>().LoadLevel("MenuScene");
    }
	void Switch()
	{
		Application.LoadLevel (1);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
