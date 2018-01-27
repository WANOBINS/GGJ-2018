using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class StevePlayerController : MonoBehaviour {

    #region Variables

    public GameObject L_Hand;
    public GameObject R_Hand;
    public VRInputManager VRIM;

    public SteamVR_TrackedController RightTouch;
    public SteamVR_TrackedController LeftTouch;

    public bool L_Grab;
    public bool R_Grab;

    private GameObject LGrabbedObject;
    private GameObject RGrabbedObject;
    public float GrabRadius;
    public LayerMask grabMask;


    #endregion Variables

    #region StartAndUpdate

    // Use this for initialization
    void Start () {

        //RightTouch = GameObject.FindGameObjectWithTag("RController").GetComponent<SteamVR_TrackedController>();
        //LeftTouch = GameObject.FindGameObjectWithTag("LController").GetComponent<SteamVR_TrackedController>();

        L_Grab = false;
        R_Grab = false;
	}
	
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("Oculus_GearVR_LThumbstickX") * .1f;
        var y = Input.GetAxis("Oculus_GearVR_LThumbstickY") * .1f;
        transform.Translate(x, 0, y);

        Grab();
	}

    #endregion StartAndUpdate

    #region GrabAndThrow

    void Grab()
    {
        if (LeftTouch.gripped == true)
        {
            L_Grab = true;
            RaycastHit[] hits;

            hits = Physics.SphereCastAll(L_Hand.transform.position, GrabRadius, transform.forward, 0f, grabMask);

            if(hits.Length > 0)
            {
                int closestHit = 0;

                for (int i = 0; i < hits.Length; i++)
                  {
                    if(hits[i].distance < hits[closestHit].distance)
                    {
                        closestHit = i;
                    }
                  }

                LGrabbedObject = hits[closestHit].transform.gameObject;
                LGrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                LGrabbedObject.transform.position = L_Hand.transform.position;
                LGrabbedObject.transform.parent = L_Hand.transform;

            }
            Debug.Log("Left Grab");
        } 

        if (RightTouch.gripped == true)
        {
            R_Grab = true;
            RaycastHit[] hits;

            hits = Physics.SphereCastAll(R_Hand.transform.position, GrabRadius, transform.forward, 0f, grabMask);

            if (hits.Length > 0)
            {
                int closestHit = 0;

                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].distance < hits[closestHit].distance)
                    {
                        closestHit = i;
                    }
                }

                RGrabbedObject = hits[closestHit].transform.gameObject;
                RGrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                RGrabbedObject.transform.position = R_Hand.transform.position;
                RGrabbedObject.transform.parent = R_Hand.transform;

            }
            Debug.Log("Right Grab");
        }

        if (LeftTouch.gripped == false && L_Grab == true)
        {
            L_Grab = false;

            if(LGrabbedObject != null)
            {
                LGrabbedObject.transform.parent = null;
                LGrabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                LGrabbedObject = null;
                
            }
                    

            
            Debug.Log("Left Let go");
        }

        if (RightTouch.gripped == false && R_Grab == true)
        {
            R_Grab = false;

            if (RGrabbedObject != null)
            {
                RGrabbedObject.transform.parent = null;
                RGrabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                RGrabbedObject = null;

            }

            
            Debug.Log("Right Let go");
        }
    }

    #endregion GrabAndThrow

}
