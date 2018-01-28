using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
    public PuzzleSequence PSq;
    
	// Use this for initialization
	void Start ()
    {
        PSq = FindObjectOfType<PuzzleSequence>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider Hand)
    {
        if (Hand.tag == "Left" || Hand.tag == "Right")
        {
            PSq.MySequence.Add(this.gameObject);

            
        }
    }
}
