using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
public class My_CursorLockManager : MonoBehaviour, IInputAxisOwner
{
    public InputAxis CursorLock = InputAxis.DefaultMomentary;

    public UnityEvent OnCursorOnStarted = new();
    public UnityEvent OnCursorLocked = new();
    public UnityEvent OnCursorUnlocked = new();
    public UnityEvent OnCursorOnGameEnded = new();

    private bool m_IsTriggered;
    private bool canPushEcs = true;

    public void GetInputAxes(List<IInputAxisOwner.AxisDescriptor> axes)
    {
        axes.Add(new()
        {
            DrivenAxis = () => ref CursorLock,
            Name = "CursorLock",
            Hint = IInputAxisOwner.AxisDescriptor.Hints.X
        });
    }

    void OnValidate() => CursorLock.Validate();
    //void OnEnable() => LockCursor();
    void OnDisable() => UnlockCursor();

    private void Start()
    {
        CursorOnStarted();
    }

    void Update()
    {
        if (CursorLock.Value == 0)
            m_IsTriggered = false;
        else if (!m_IsTriggered && canPushEcs)
        {
            m_IsTriggered = true;
            if (Cursor.lockState == CursorLockMode.None)
                LockCursor();
            else
                UnlockCursor();
        }
    }

    public void CursorOnStarted()
    {
        Cursor.lockState = CursorLockMode.None;
        SetCanPushEcs(false);
        OnCursorOnStarted?.Invoke();
    }

    public void LockCursor()
    {
        if (enabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            OnCursorLocked?.Invoke();
        }
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        OnCursorUnlocked?.Invoke();
    }

    public void SetCanPushEcs(bool canPushEcs) => this.canPushEcs = canPushEcs;

    public void CursorOnGameEnded()
    {
        Cursor.lockState = CursorLockMode.None;
        OnCursorOnGameEnded?.Invoke();
    }
}
