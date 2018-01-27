using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class StevePlayerController : MonoBehaviour {

    #region Variables

    public GameObject L_Hand;
    public GameObject R_Hand;

    #endregion Variables

    // Use this for initialization
    void Start () {
        L_Hand = GameObject.FindGameObjectWithTag("Left");
        R_Hand = GameObject.FindGameObjectWithTag("Right");
	}
	
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        transform.Translate(x, 0, y);
	}
}
