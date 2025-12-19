using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCount : MonoBehaviour
{
    private List<Target> listOfTarget;

    private Action<GameSceneController.GameEndType> targetAllOut;

    private void Awake()
    {
        listOfTarget = new List<Target>();
    }

    public void Intialized(Action<GameSceneController.GameEndType> targellAllOutCallback)
    {
        targetAllOut = targellAllOutCallback;
    }

    private void Start()
    {
        Target[] targetArray = FindObjectsByType<Target>(FindObjectsSortMode.None);
        
        if (targetArray.Length > 0)
        {
            foreach(Target target in targetArray)
            {
                target.Initialized(OnTargetHit);
                listOfTarget.Add(target);
            }
        }
    }

    private void OnTargetHit(Target target)
    {
        listOfTarget.Remove(target);
        Destroy(target.gameObject);

        if (listOfTarget.Count <= 0)
        {
            targetAllOut?.Invoke(GameSceneController.GameEndType.ShootAll);
        }
    }
}
