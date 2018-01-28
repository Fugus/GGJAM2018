using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaquotFakeGravity : MonoBehaviour
{
    public Transform bouleJaquot;

    [SerializeField]
    private float gravityFactor = 1f;

    public Vector3 fakeGravityDirection;

    void FixedUpdate()
    {
        Rigidbody myRigidBody = GetComponent<Rigidbody>();
        fakeGravityDirection = (bouleJaquot.position - (myRigidBody.position - transform.up)).normalized;
        myRigidBody.AddForceAtPosition((gravityFactor + myRigidBody.velocity.magnitude) * Physics.gravity.magnitude * fakeGravityDirection.normalized, myRigidBody.position - transform.up, ForceMode.Force);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, fakeGravityDirection);
    }
}
