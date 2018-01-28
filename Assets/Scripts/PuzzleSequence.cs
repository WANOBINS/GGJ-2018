﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSequence : MonoBehaviour
{

    public bool teleporterPower = false;
    public ParticleSystem teleparticle;
    public GameObject LGButt;
    public GameObject SGButt;
    public GameObject LGSwitch;
    public GameObject SGSwitch;
    public GameObject GCrystal;

    public GameObject LRButt; // FIRE Button
    public GameObject SRButt;
    public GameObject LRSwitch;
    public GameObject SRSwitch;
    public GameObject RCrystal;

    public GameObject LBButt;
    public GameObject SBButt;
    public GameObject LBSwitch;
    public GameObject SBSwitch;
    public GameObject BCrystal;

    public GameObject[] MySequence;

    // Use this for initialization
    void Start ()
    {
        GameObject[] CorrectSequence = { LGButt, SBSwitch, RCrystal, SBButt };
        
	}
	
	// Update is called once per frame
	void Update ()
    { 
		//SBButt.GetComponent<Collider>
	}

   /* public PuzzleSequence PSq;
    PSq = FindObjectOfType<PuzzleSequence>();*/

    private void OnTriggerEnter(Collider Hand)
    {
        if(Hand.tag == "Left"|| Hand.tag == "Right")
        {
            
        }
    }
}