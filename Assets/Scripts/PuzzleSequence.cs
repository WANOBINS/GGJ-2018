using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSequence : MonoBehaviour
{

    public bool teleporterPower = false;
    public ParticleSystem teleparticle;
    public ParticleSystem earthExplode;


    public  GameObject LGButt;
    public  GameObject SGButt;
    public  GameObject LGSwitch;
    public  GameObject SGSwitch;
    public  GameObject GCrystal;

    public  GameObject LRButt; // FIRE Button
    public  GameObject SRButt;
    public  GameObject LRSwitch;
    public  GameObject SRSwitch;
    public  GameObject RCrystal;

    public  GameObject LBButt;
    public  GameObject SBButt;
    public  GameObject LBSwitch;
    public  GameObject SBSwitch;
    public  GameObject BCrystal;

    // public GameObject[] MySequence;
    public List<GameObject> MySequence;
    public List<GameObject> CorrectSequence;
    

    // Use this for initialization
    void Start ()
    {
        CorrectSequence.Add(LGButt);
        CorrectSequence.Add(SBSwitch);
        CorrectSequence.Add(RCrystal);
        CorrectSequence.Add(SBButt);
        CorrectSequence.Add(LRButt);
    }

    // Update is called once per frame
    void Update()
    {
        //SBButt.GetComponent<Collider>
        if (MySequence[MySequence.Count - 1] == LRButt)
        {
            if(MySequence == CorrectSequence)
            {
                teleporterPower = true;
                //teleparticle.emission.enabled
                //enable emission
            }
            else
            {
                //Blow up earth
            }
        }        
	}    
}
