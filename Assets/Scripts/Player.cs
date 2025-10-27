using UnityEngine;

public class Player : MonoBehaviour
{
    public StateMachine stateMachine { get; private set; }
    public Animator anim { get; private set; }
    public Rigidbody rb { get; private set; }
    public Vector2 moveInput { get; private set; }

    #region Player' State
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    #endregion

    [Header("Movement Details")]
    public float moveSpeed;

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
    }

    public void SetVelocity(float xVelocity, float zVelocity)
    {
        rb.linearVelocity = new Vector3(xVelocity * moveSpeed, rb.linearVelocity.y, zVelocity * moveSpeed);
    }
}
