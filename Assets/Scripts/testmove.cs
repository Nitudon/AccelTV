﻿using UnityEngine;
using System.Collections;

public class testmove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.F))
        {
            GetComponent<Animator>().SetTrigger("test");
        }

        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Animator>().SetInteger("num",1);
        }
    }
}
