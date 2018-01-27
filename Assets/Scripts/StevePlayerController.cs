using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class StevePlayerController : MonoBehaviour {

    #region Variables

    public GameObject L_Hand;
    public GameObject R_Hand;
    public VRInputManager VRIM;

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
        L_Hand = GameObject.FindGameObjectWithTag("Left");
        R_Hand = GameObject.FindGameObjectWithTag("Right");

        VRIM = GetComponent<VRInputManager>();

        L_Grab = false;
        R_Grab = false;
	}
	
	// Update is called once per frame
	void Update () {
        var x = Input.GetAxis("Oculus_GearVR_LThumbstickX");
        var y = Input.GetAxis("Oculus_GearVR_LThumbstickY");
        transform.Translate(x, 0, y);

        Grab();
	}

    #endregion StartAndUpdate

    #region GrabAndThrow

    void Grab()
    {
        if (VRIM.LDevice.GetPressDown(EVRButtonId.k_EButton_Grip))
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
            Debug.Log("Grab");
        }

        if (VRIM.RDevice.GetPressDown(EVRButtonId.k_EButton_Grip))
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
            Debug.Log("Grab");
        }

        if (VRIM.LDevice.GetPressUp(EVRButtonId.k_EButton_Grip) && L_Grab == true)
        {
            L_Grab = false;

            if(LGrabbedObject != null)
            {
                LGrabbedObject.transform.parent = null;
                LGrabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                LGrabbedObject = null;
                
            }
                    

            
            Debug.Log("Let go");
        }

        if (VRIM.RDevice.GetPressUp(EVRButtonId.k_EButton_Grip) && R_Grab == true)
        {
            R_Grab = false;

            if (RGrabbedObject != null)
            {
                RGrabbedObject.transform.parent = null;
                RGrabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                RGrabbedObject = null;

            }

            
            Debug.Log("Let go");
        }
    }

    #endregion GrabAndThrow

}
