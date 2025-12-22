using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI targetCountText;
    [SerializeField] private bool destroyTargetOnHit = true;

    private List<Target> listOfTarget;

    private Action<GameSceneController.GameEndType> targetAllOut;
    private int allTarget;

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

                if (target.GetScore > 0)
                    listOfTarget.Add(target);
            }
        }

        allTarget = listOfTarget.Count;
        UpdateText();
    }

    private void OnTargetHit(Target target)
    {
        if (destroyTargetOnHit)
        {
            if (target.GetScore > 0)
                listOfTarget.Remove(target);

            Destroy(target.gameObject);
        }

        UpdateText();

        if (listOfTarget.Count <= 0)
        {
            targetAllOut?.Invoke(GameSceneController.GameEndType.ShootAll);
        }
    }

    private void UpdateText()
    {
        targetCountText.text = $"{listOfTarget.Count}/{allTarget}";
    }
}
