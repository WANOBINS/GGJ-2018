using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollisionDebugMovement : MonoBehaviour
{
    #region Enums

    public enum RotationAction
    {
        None,
        Left,
        Right
    }

    public enum LRMovementAction
    {
        None,
        Left,
        Right
    }

    public enum FBMovementAction
    {
        None,
        Fore,
        Back
    }

    #endregion Enums

    #region Classes

    public struct MovementState
    {
        public MovementState(RotationAction RotationState, FBMovementAction FBState, LRMovementAction LRState)
        {
            this.RotationState = RotationState;
            this.FBState = FBState;
            this.LRState = LRState;
        }

        public override string ToString()
        {
            return "Rotation State: " + RotationState + " FBState: " + FBState + " LRState: " + LRState;
        }

        public RotationAction RotationState { get; private set; }
        public FBMovementAction FBState { get; private set; }
        public LRMovementAction LRState { get; private set; }

        public static MovementState Zero { get { return new MovementState(RotationAction.None, FBMovementAction.None, LRMovementAction.None); } }

        public void MakeZero()
        {
            this.RotationState = RotationAction.None;
            this.FBState = FBMovementAction.None;
            this.LRState = LRMovementAction.None;
        }

        public void UpdateState()
        {
            //Rotation State
            if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.E))
            {
                RotationState = RotationAction.None;
            }
            else
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    RotationState = RotationAction.Left;
                }
                if (Input.GetKey(KeyCode.E))
                {
                    RotationState = RotationAction.Right;
                }
            }

            //Forward/Backward Movement State
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            {
                FBState = FBMovementAction.None;
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    FBState = FBMovementAction.Fore;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    FBState = FBMovementAction.Back;
                }
            }

            //Left/Right Movement State
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            {
                LRState = LRMovementAction.None;
            }
            else
            {
                if (Input.GetKey(KeyCode.A))
                {
                    LRState = LRMovementAction.Left;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    LRState = LRMovementAction.Right;
                }
            }
        }
    }

    #endregion Classes

    #region Variables

    private MovementState movementState = MovementState.Zero;

    public float RotationSpeed;
    public float MovementSpeed;
    public float JumpForce;

    private new Rigidbody rigidbody;

    #endregion Variables

    #region Unity Functions

    // Use this for initialization
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        if (!rigidbody)
        {
            Debug.LogError("CollisionDebugMovement requires a Rigidbody to work!");
        }
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddForce(Vector3.up * JumpForce);
        }
        rigidbody.AddForce(-Vector3.up * 100);
        movementState.UpdateState();
        ActOnInputStates();
        movementState.MakeZero();
    }

    #endregion Unity Functions

    #region Helper Functions

    private void ActOnInputStates()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 angularVelocity = Vector3.zero;
        switch (movementState.RotationState)
        {
            case RotationAction.None:
                angularVelocity = Vector3.zero;
                break;

            case RotationAction.Left:
                angularVelocity = new Vector3(0, -RotationSpeed, 0);
                break;

            case RotationAction.Right:
                angularVelocity = new Vector3(0, RotationSpeed, 0);
                break;
        }

        switch (movementState.FBState)
        {
            case FBMovementAction.None:
                velocity += transform.forward * 0;
                break;

            case FBMovementAction.Fore:
                velocity += transform.forward * MovementSpeed;
                break;

            case FBMovementAction.Back:
                velocity += -transform.forward * MovementSpeed;
                break;
        }

        switch (movementState.LRState)
        {
            case LRMovementAction.None:
                velocity += transform.right * 0;
                break;

            case LRMovementAction.Left:
                velocity += -transform.right * MovementSpeed;
                break;

            case LRMovementAction.Right:
                velocity += transform.right * MovementSpeed;
                break;
        }

        rigidbody.velocity = velocity;
        rigidbody.angularVelocity = angularVelocity;
    }

    #endregion Helper Functions
}