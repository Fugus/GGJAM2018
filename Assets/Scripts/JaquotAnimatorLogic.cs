using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaquotAnimatorLogic : MonoBehaviour
{
    public Animator animator;
    public float maxWalkSpeed = 1.5f;

    public float minWalkSpeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsWalking", GetComponent<Rigidbody>().velocity.magnitude > minWalkSpeed);
        float remappedSpeed = (GetComponent<Rigidbody>().velocity.magnitude - minWalkSpeed) / (maxWalkSpeed - minWalkSpeed);
        remappedSpeed = Mathf.Max(minWalkSpeed, remappedSpeed);
        animator.SetFloat("Speed", remappedSpeed);
    }
}
