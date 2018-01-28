using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointLogic : MonoBehaviour
{
    public float durationToAdd = 10f;

    bool hasBeenChecked = false;

    public UnityEvent OnChecked;
    public UnityEvent OnReset;

    public void Reset()
    {
        hasBeenChecked = false;
        OnReset.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenChecked)
        {
            FindObjectOfType<CheckpointManager>().remainingTime += durationToAdd;
            OnChecked.Invoke();
        }
        hasBeenChecked = true;
    }
}
