﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TestTrans : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
        SceneManager.LoadScene("");
	}
}