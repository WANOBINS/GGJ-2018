using Assets.Scripts.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    GameObject Singleton;
	// Use this for initialization
	void Start () {
        RunSingletonCheck();
    }

    private void RunSingletonCheck()
    {
        Singleton = new GameObject
        {
            name = "Singleton"
        };
        Singleton.AddComponent<Singleton>();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
