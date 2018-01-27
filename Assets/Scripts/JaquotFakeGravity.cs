using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaquotFakeGravity : MonoBehaviour
{

    [SerializeField]
    private Transform bouleJaquot;

    [SerializeField]
    [Tooltip("")]
    private float gravityFactor;

    public Vector3 fakeGravityDirection;

    void FixedUpdate()
    {
        Rigidbody myRigidBody = GetComponent<Rigidbody>();
        fakeGravityDirection = (bouleJaquot.position - (myRigidBody.position - transform.up)).normalized;
        myRigidBody.AddForceAtPosition(gravityFactor * Physics.gravity.magnitude * fakeGravityDirection.normalized, myRigidBody.position - 10 * transform.up, ForceMode.Force);
        //myRigidBody.rotation = Quaternion.LookRotation(transform.forward, -fakeGravityDirection);
		

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, fakeGravityDirection);
    }
}
