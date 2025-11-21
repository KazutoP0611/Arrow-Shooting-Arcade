using Unity.Cinemachine.Samples;
using UnityEngine;

public class My_AnimatorController : MonoBehaviour
{
    protected struct AnimationParams
    {
        public bool IsWalking;
        public bool IsRunning;
        public bool IsJumping;
        public bool LandTriggered;
        public bool JumpTriggered;
        public Vector3 Direction; // normalized direction of motion
        public float MotionScale; // scale factor for the animation speed
        public float JumpScale; // scale factor for the jump animation

        //My modified
        public bool IsAiming;
    }

    [Tooltip("Tune this to the animation in the model: feet should not slide when walking at this speed")]
    public float NormalWalkSpeed = 1.7f;

    [Tooltip("Tune this to the animation in the model: feet should not slide when sprinting at this speed")]
    public float NormalSprintSpeed = 5;

    [Tooltip("Never speed up the sprint animation more than this, to avoid absurdly fast movement")]
    public float MaxSprintScale = 1.4f;

    public My_AimController m_AimController;
    //[Tooltip("Scale factor for the overall speed of the jump animation")]
    //public float JumpAnimationScale = 0.65f;

    private const float k_IdleThreshold = 0.2f;

    private My_PlayerController m_Controller;
    private AnimationParams m_AnimationParams;
    private Vector3 m_PreviousPosition;

    private void Start()
    {
        m_PreviousPosition = transform.position;

        m_Controller = GetComponentInParent<My_PlayerController>();
        //m_AimController = GetComponent<My_AimController>();

        if (m_Controller != null)
        {
            // Install our callbacks to handle jump and animation based on velocity
            //Debug.LogWarning("Heck yeah!!");
            m_Controller.StartJump += () => m_AnimationParams.JumpTriggered = true;
            m_Controller.EndJump += () => m_AnimationParams.LandTriggered = true;
            m_Controller.PostUpdate += (vel, jumpAnimationScale) => UpdateAnimationState(vel, jumpAnimationScale);
        }
    }

    private void LateUpdate()
    {
        // In no-controller mode, we monitor the player's motion and deduce the appropriate animation.
        // We don't support jumping in this mode.
        if (m_Controller == null || !m_Controller.enabled)
        {
            // Get velocity in player-local coords
            var pos = transform.position;
            var vel = Quaternion.Inverse(transform.rotation) * (pos - m_PreviousPosition) / Time.deltaTime;
            m_PreviousPosition = pos;
            UpdateAnimationState(vel, 1);
        }
    }

    void UpdateAnimationState(Vector3 vel, float jumpAnimationScale)
    {
        vel.y = 0; // we don't consider vertical movement
        var speed = vel.magnitude;

        // Hysteresis reduction
        bool isRunning = speed > NormalWalkSpeed * 2 + (m_AnimationParams.IsRunning ? -0.15f : 0.15f);
        bool isWalking = !isRunning && speed > k_IdleThreshold + (m_AnimationParams.IsWalking ? -0.05f : 0.05f);
        m_AnimationParams.IsWalking = isWalking;
        m_AnimationParams.IsRunning = isRunning;

        // Set the normalized direction of motion and scale the animation speed to match motion speed
        m_AnimationParams.Direction = speed > k_IdleThreshold ? vel / speed : Vector3.zero;
        m_AnimationParams.MotionScale = isWalking ? speed / NormalWalkSpeed : 0;//1;
        //m_AnimationParams.JumpScale = JumpAnimationScale * jumpAnimationScale;

        // We scale the sprint animation speed to loosely match the actual speed, but we cheat
        // at the high end to avoid making the animation look ridiculous
        if (isRunning)
            m_AnimationParams.MotionScale = (speed < NormalSprintSpeed)
                ? speed / NormalSprintSpeed
                : Mathf.Min(MaxSprintScale, 1 + (speed - NormalSprintSpeed) / (3 * NormalSprintSpeed));

        m_AnimationParams.IsAiming = m_AimController.PlayerRotation == My_AimController.CouplingMode.Coupled;

        UpdateAnimation(m_AnimationParams);

        //if (m_AnimationParams.JumpTriggered)
        //    m_AnimationParams.IsJumping = true;
        //if (m_AnimationParams.LandTriggered)
        //    m_AnimationParams.IsJumping = false;

        //m_AnimationParams.JumpTriggered = false;
        //m_AnimationParams.LandTriggered = false;
    }

    protected virtual void UpdateAnimation(AnimationParams animationParams)
    {
        if (!TryGetComponent(out Animator animator))
        {
            Debug.LogError("SimplePlayerAnimator: An Animator component is required");
            return;
        }

        animator.SetFloat("DirX", animationParams.Direction.x);
        animator.SetFloat("DirZ", animationParams.Direction.z);
        animator.SetFloat("MotionScale", animationParams.MotionScale);
        animator.SetBool("Walking", animationParams.IsWalking);
        animator.SetBool("Running", animationParams.IsRunning);
        animator.SetBool("Aiming", animationParams.IsAiming);
        //animator.SetFloat("JumpScale", animationParams.JumpScale);

        //if (m_AnimationParams.JumpTriggered)
        //    animator.SetTrigger("Jump");
        //if (m_AnimationParams.LandTriggered)
        //    animator.SetTrigger("Land");
    }
}
