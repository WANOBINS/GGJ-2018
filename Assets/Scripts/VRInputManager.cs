using System;
using UnityEngine;

public class VRInputManager : MonoBehaviour
{
    #region Variables

    [Header("Controller Object Tags")]
    [Tooltip("Used to find the LControllerObject if not set below")]
    [SerializeField]
    private String LControllerObjectTag = "LController";
    [Tooltip("Used to find the RControllerObject if not set below")]
    [SerializeField]
    private String RControllerObjectTag = "RController";

    [Header("Controller Transforms")]
    [Tooltip("Set your LControllerObject here")]
    public Transform LControllerObject;
    [Tooltip("Set your RControllerObject here")]
    public Transform RControllerObject;

    protected SteamVR_TrackedController LController;
    protected SteamVR_TrackedController RController;

    public SteamVR_Controller.Device LDevice;
    public SteamVR_Controller.Device RDevice;

    protected Controller MissingControllers;

    public enum Controller
    {
        None,
        Both,
        Left,
        Right
    }

    #endregion Variables

    #region Contructors

    private VRInputManager(GameObject LControllerObject, GameObject RControllerObject)
    {
        this.LControllerObject = LControllerObject.transform;
        this.RControllerObject = RControllerObject.transform;
    }

    private VRInputManager(Transform LControllerObject, Transform RControllerObject)
    {
        this.LControllerObject = LControllerObject;
        this.RControllerObject = RControllerObject;
    }

    #endregion Contructors

    #region Unity Functions

    // Use this for initialization
    private void Start()
    {
        GetMissingControllers();
        if (MissingControllers != Controller.None)
        {
            AttemptFindMissingControllers();
        }
        getDevices();
    }

    #endregion Unity Functions

    #region Other Functions

    private void AttemptFindMissingControllers()
    {
        switch (MissingControllers)
        {
            case Controller.None:
                break;

            case Controller.Both:
                Debug.LogWarning("Missing references to both controllers, attempting to find via standard names");
                SetControllerObjects(GameObject.Find("Controller (left)"), GameObject.Find("Controller (right)"));
                GetMissingControllers();
                switch (MissingControllers)
                {
                    case Controller.None:
                        {
                            Debug.LogWarning("Found both controllers by name");
                            break;
                        }

                    case Controller.Both:
                        {
                            Debug.LogWarning("Could not find controllers by name, trying by tag");
                            try
                            {
                                SetControllerObjects(GameObject.FindGameObjectWithTag(LControllerObjectTag), GameObject.FindGameObjectWithTag(RControllerObjectTag));
                            }
                            catch(Exception ex)
                            {

                            }
                            GetMissingControllers();
                            switch (MissingControllers)
                            {
                                case Controller.None:
                                    {
                                        Debug.LogWarning("Found all controllers by tag");
                                        break;
                                    }

                                case Controller.Both:
                                    {
                                        Debug.LogWarning("Could not find controllers by tag, please set them via the inspector");
                                        break;
                                    }

                                case Controller.Left:
                                    {
                                        FailLControllerObject();
                                        break;
                                    }

                                case Controller.Right:
                                    {
                                        FailRControllerObject();
                                        break;
                                    }
                            }
                            break;
                        }

                    case Controller.Left:
                        {
                            Debug.LogWarning("Found RControllerObject by name, attempting to find LControllerObject by tag");
                            try
                            {
                                RControllerObject = GameObject.FindGameObjectWithTag(LControllerObjectTag).transform;
                            }
                            catch (Exception ex)
                            {

                            }
                            GetMissingControllers();
                            switch (MissingControllers)
                            {
                                case Controller.Left:
                                    {
                                        FailLControllerObject();
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                            break;
                        }

                    case Controller.Right:
                        Debug.LogWarning("Found LControllerObject by name, attempting to find RControllerObject by tag");
                        try
                        {
                            RControllerObject = GameObject.FindGameObjectWithTag(RControllerObjectTag).transform;
                        }
                        catch(Exception ex)
                        {

                        }
                        GetMissingControllers();
                        switch (MissingControllers)
                        {
                            case Controller.Right:
                                {
                                    FailRControllerObject();
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        break;
                }
                break;

            case Controller.Left:
                Debug.LogWarning("Could not find LControllerObject by standard name, attempting to find by tag");
                try
                {
                    LControllerObject = GameObject.FindGameObjectWithTag(LControllerObjectTag).transform;
                }
                catch(Exception ex)
                {

                }
                GetMissingControllers();
                switch (MissingControllers)
                {
                    case Controller.Left:
                        {
                            FailLControllerObject();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                break;

            case Controller.Right:
                Debug.LogWarning("Could not find RControllerObject by standard name, attempting to find by tag");
                try
                {
                    LControllerObject = GameObject.FindGameObjectWithTag(RControllerObjectTag).transform;
                }
                catch(Exception ex)
                {

                }
                GetMissingControllers();
                switch (MissingControllers)
                {
                    case Controller.Right:
                        {
                            FailRControllerObject();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                break;
        }
    }

    private static void FailRControllerObject()
    {
        Debug.LogWarning("Could not find RControllerObject by tag, please set via inspector");
    }

    private static void FailLControllerObject()
    {
        Debug.LogWarning("Could not find LControllerObject by tag, plese set via inspector");
    }

    private void getDevices()
    {
        try
        {
            LDevice = SteamVR_Controller.Input((int)LController.controllerIndex);
        }
        catch(Exception ex)
        {

        }
        try
        {
            RDevice = SteamVR_Controller.Input((int)RController.controllerIndex);
        }
        catch (Exception ex)
        {

        }
    }

    private void GetTrackedControllers()
    {
        LController = LControllerObject.GetComponent<SteamVR_TrackedController>();
        RController = RControllerObject.GetComponent<SteamVR_TrackedController>();
    }

    public void SetControllerObjects(GameObject LControllerObject, GameObject RControllerObject)
    {
        if (LControllerObject)
        {
            this.LControllerObject = LControllerObject.transform;
        }
        if (RControllerObject)
        {
            this.RControllerObject = RControllerObject.transform;
        }
        getDevices();
    }

    public void SetControllerObjects(Transform LControllerObject, Transform RControllerObject)
    {
        if (LControllerObject)
        {
            this.LControllerObject = LControllerObject;
        }
        if (RControllerObject)
        {
            this.RControllerObject = RControllerObject;
        }
        getDevices();
    }

    private void GetMissingControllers()
    {
        if (!LControllerObject && !RControllerObject)
        {
            MissingControllers = Controller.Both;
        }
        else if (!LControllerObject)
        {
            MissingControllers = Controller.Left;
        }
        else if (!RControllerObject)
        {
            MissingControllers = Controller.Right;
        }
        else
        {
            MissingControllers = Controller.None;
        }
    }

    #endregion Other Functions
}