using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public abstract class My_PlayerControllerBase : MonoBehaviour, Unity.Cinemachine.IInputAxisOwner
{
    public Action PreUpdate;
    public Action<Vector3, float> PostUpdate;

    [Header("Input Axes")]
    [Tooltip("X Axis movement.  Value is -1..1.  Controls the sideways movement")]
    public InputAxis MoveX = InputAxis.DefaultMomentary;

    [Tooltip("Z Axis movement.  Value is -1..1. Controls the forward movement")]
    public InputAxis MoveZ = InputAxis.DefaultMomentary;

    //[Tooltip("Jump movement.  Value is 0 or 1. Controls the vertical movement")]
    //public InputAxis Jump = InputAxis.DefaultMomentary;

    [Tooltip("Sprint movement.  Value is 0 or 1. If 1, then is sprinting")]
    public InputAxis Sprint = InputAxis.DefaultMomentary;

    public virtual void SetStrafeMode(bool b) { }
    public abstract bool IsMoving { get; }

    public void GetInputAxes(List<IInputAxisOwner.AxisDescriptor> axes)
    {
        axes.Add(new() { DrivenAxis = () => ref MoveX, Name = "Move X", Hint = IInputAxisOwner.AxisDescriptor.Hints.X });
        axes.Add(new() { DrivenAxis = () => ref MoveZ, Name = "Move Z", Hint = IInputAxisOwner.AxisDescriptor.Hints.Y });
        //axes.Add(new() { DrivenAxis = () => ref Jump, Name = "Jump" });
        axes.Add(new() { DrivenAxis = () => ref Sprint, Name = "Sprint" });
    }

    protected virtual void OnValidate()
    {
        MoveX.Validate();
        MoveZ.Validate();
        //Jump.Validate();
        Sprint.Validate();
    }
}

public class My_PlayerController : My_PlayerControllerBase
{
    public override bool IsMoving => m_LastInput.sqrMagnitude > 0.01f;

    private Vector3 m_LastInput;
}