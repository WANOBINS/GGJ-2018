using UnityEngine;

[OneInScene]
public class VRInputManager : MonoBehaviour
{
    #region Variables

    public Transform LControllerObject;
    public Transform RControllerObject;

    protected SteamVR_TrackedController LController;
    protected SteamVR_TrackedController RController;

    protected SteamVR_Controller.Device LDevice;
    protected SteamVR_Controller.Device RDevice;

    #endregion Variables

    #region Unity Functions

    // Use this for initialization
    private void Start()
    {
        GetTrackedControllers();
        getDevices();
    }

    #endregion Unity Functions

    #region Menial Functions

    private void getDevices()
    {
        LDevice = SteamVR_Controller.Input((int)LController.controllerIndex);
        RDevice = SteamVR_Controller.Input((int)RController.controllerIndex);
    }

    private void GetTrackedControllers()
    {
        LController = LControllerObject.GetComponent<SteamVR_TrackedController>();
        RController = RControllerObject.GetComponent<SteamVR_TrackedController>();
    }

    #endregion Menial Functions
}