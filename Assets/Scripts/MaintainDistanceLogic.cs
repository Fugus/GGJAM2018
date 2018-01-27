using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainDistanceLogic : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    public float desiredDistance = 1f;

    void FixedUpdate()
    {
        float distToTarget = Vector3.Distance(target.position, transform.position);
		
		
    }
}
