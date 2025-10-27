using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    public Animator anim { get; private set; }
    public Rigidbody rb { get; private set; }
    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }

    #region Player' State
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    #endregion

    [Header("Movement Details")]
    public float moveSpeed;
    public float cameraPanSpeed;

    private ArcherInputActions input;

    private void Awake()
    {
        stateMachine = new StateMachine();
        input = new ArcherInputActions();

        rb = GetComponent<Rigidbody>();

        idleState = new Player_IdleState(this, stateMachine, "Idle");
        moveState = new Player_MoveState(this, stateMachine, "Move");
    }

    private void OnEnable()
    {
        input.Enable();

        input.Character.Move.performed += value => moveInput = value.ReadValue<Vector2>();
        input.Character.Move.canceled += value => moveInput = Vector2.zero;

        input.Character.Look.performed += value => lookInput = value.ReadValue<Vector2>();
        input.Character.Look.canceled += value => lookInput = Vector2.zero;
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Start()
    {
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        Look();
    }

    private void Look()
    {
        if (lookInput.x == 0 && lookInput.y == 0)
            return;

        transform.rotation = Quaternion.Euler(new Vector3(0, lookInput.x * cameraPanSpeed, 0));
    }

    public void SetVelocity(float xVelocity, float zVelocity)
    {
        rb.linearVelocity = new Vector3(xVelocity * moveSpeed, rb.linearVelocity.y, zVelocity * moveSpeed);
    }
}
