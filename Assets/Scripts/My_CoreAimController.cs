using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using static Unity.Cinemachine.Samples.SimplePlayerAimController;

public class My_CoreAimController : MonoBehaviour, IInputAxisOwner
{
    [Tooltip("How fast the player rotates to face the camera direction when the player starts moving.  "
            + "Only used when Player Rotation is Coupled When Moving.")]
    public float RotationDamping = 0.2f;

    [Tooltip("Horizontal Rotation.  Value is in degrees, with 0 being centered.")]
    public InputAxis HorizontalLook = new() { Range = new Vector2(-180, 180), Wrap = true, Recentering = InputAxis.RecenteringSettings.Default };

    [Tooltip("Vertical Rotation.  Value is in degrees, with 0 being centered.")]
    public InputAxis VerticalLook = new() { Range = new Vector2(-70, 70), Recentering = InputAxis.RecenteringSettings.Default };

    public CouplingMode PlayerRotation;

    private My_PlayerController m_Controller;
    private Transform m_ControllerTransform;
    private Quaternion m_DesiredWorldRotation;

    public void GetInputAxes(List<IInputAxisOwner.AxisDescriptor> axes)
    {
        axes.Add(new() { DrivenAxis = () => ref HorizontalLook, Name = "Horizontal Look", Hint = IInputAxisOwner.AxisDescriptor.Hints.X });
        axes.Add(new() { DrivenAxis = () => ref VerticalLook, Name = "Vertical Look", Hint = IInputAxisOwner.AxisDescriptor.Hints.Y });
    }

    private void OnValidate()
    {
        HorizontalLook.Validate();
        VerticalLook.Range.x = Mathf.Clamp(VerticalLook.Range.x, -90, 90);
        VerticalLook.Range.y = Mathf.Clamp(VerticalLook.Range.y, -90, 90);
        VerticalLook.Validate();
    }

    private void OnEnable()
    {
        m_Controller = GetComponentInParent<My_PlayerController>();
        if (m_Controller == null)
            Debug.LogError("SimplePlayerController not found on parent object");
        else
        {
            m_Controller.PreUpdate -= UpdatePlayerRotation;
            m_Controller.PreUpdate += UpdatePlayerRotation;
            m_Controller.PostUpdate -= PostUpdate;
            m_Controller.PostUpdate += PostUpdate;
            m_ControllerTransform = m_Controller.transform;
        }
    }

    // This is called by the player controller before it updates its own rotation.
    void UpdatePlayerRotation()
    {
        var t = transform;
        t.localRotation = Quaternion.Euler(VerticalLook.Value, HorizontalLook.Value, 0);
        m_DesiredWorldRotation = t.rotation;
        switch (PlayerRotation)
        {
            case CouplingMode.Coupled:
                {
                    m_Controller.SetStrafeMode(true);
                    RecenterPlayer();
                    break;
                }
            case CouplingMode.CoupledWhenMoving:
                {
                    // If the player is moving, rotate its yaw to match the camera direction,
                    // otherwise let the camera orbit
                    m_Controller.SetStrafeMode(true);
                    if (m_Controller.IsMoving)
                        RecenterPlayer(RotationDamping);
                    break;
                }
            case CouplingMode.Decoupled:
                {
                    m_Controller.SetStrafeMode(false);
                    break;
                }
        }
        VerticalLook.UpdateRecentering(Time.deltaTime, VerticalLook.TrackValueChange());
        HorizontalLook.UpdateRecentering(Time.deltaTime, HorizontalLook.TrackValueChange());
    }

    // Callback for player controller to update our rotation after it has updated its own.
    private void PostUpdate(Vector3 vel, float speed)
    {
        if (PlayerRotation == CouplingMode.Decoupled)
        {
            // After player has been rotated, we subtract any rotation change
            // from our own transform, to maintain our world rotation
            transform.rotation = m_DesiredWorldRotation;
            var delta = (Quaternion.Inverse(m_ControllerTransform.rotation) * m_DesiredWorldRotation).eulerAngles;
            VerticalLook.Value = NormalizeAngle(delta.x);
            HorizontalLook.Value = NormalizeAngle(delta.y);
        }
    }

    public void RecenterPlayer(float damping = 0)
    {
        if (m_ControllerTransform == null)
            return;

        // Get my rotation relative to parent
        var rot = transform.localRotation.eulerAngles;
        rot.y = NormalizeAngle(rot.y);
        var delta = rot.y;
        delta = Damper.Damp(delta, damping, Time.deltaTime);

        // Rotate the parent towards me
        m_ControllerTransform.rotation = Quaternion.AngleAxis(
            delta, m_ControllerTransform.up) * m_ControllerTransform.rotation;

        // Rotate me in the opposite direction
        HorizontalLook.Value -= delta;
        rot.y -= delta;
        transform.localRotation = Quaternion.Euler(rot);
    }

    private float NormalizeAngle(float angle)
    {
        while (angle > 180)
            angle -= 360;
        while (angle < -180)
            angle += 360;
        return angle;
    }
}
