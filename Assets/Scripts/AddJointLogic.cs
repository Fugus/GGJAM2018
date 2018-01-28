using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddJointLogic : MonoBehaviour
{
    [SerializeField]
    private float padding = 1.2f;

    [SerializeField]
    private Rigidbody smallBall;

    [SerializeField]
    private ConfigurableJoint bonGarsToSmallBallJoint;

    [SerializeField]
    private Transform bouleJaquot;

    [SerializeField]
    private LayerMask validLayersForCollision;

    float scaleRatioBigBallToJaquotBoule = 1f;

    private void Start()
    {
        float bigBallScale = GetComponent<Collider>().bounds.extents.magnitude;
        float bouleJaquotScale = bouleJaquot.GetComponent<Collider>().bounds.extents.magnitude;
        scaleRatioBigBallToJaquotBoule = bigBallScale / bouleJaquotScale;
        Debug.LogFormat(gameObject, "scaleRatioBigBallToJaquotBoule {0}", scaleRatioBigBallToJaquotBoule);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (validLayersForCollision.value == (validLayersForCollision.value | (1 << other.gameObject.layer)))
        {
            Debug.LogFormat(gameObject, "Collided with {0}", other.transform.GetHierarchyPath());

            // Adding to bouleJaquot
            {
                GameObject jaquotOther = GameObject.Instantiate(other.gameObject);
                Vector3 bigBallToOtherPosition = transform.InverseTransformDirection(other.rigidbody.position - transform.position);

                Vector3 bigBallToOtherUp = transform.InverseTransformDirection(other.transform.up);
                Vector3 bigBallToOtherForward = transform.InverseTransformDirection(other.transform.forward);

                Destroy(jaquotOther.GetComponent<Rigidbody>());//.useGravity = false;
                //JaquotFakeGravity otherFakeGravitiy = jaquotOther.AddComponent<JaquotFakeGravity>();
                //otherFakeGravitiy.bouleJaquot = bouleJaquot.transform;
                //jaquotOther.transform.SetParent(bouleJaquot.transform);
                jaquotOther.transform.position = bouleJaquot.position + bouleJaquot.transform.TransformDirection(bigBallToOtherPosition) / scaleRatioBigBallToJaquotBoule;
                jaquotOther.transform.rotation = Quaternion.LookRotation(bouleJaquot.transform.TransformDirection(bigBallToOtherForward), bouleJaquot.transform.TransformDirection(bigBallToOtherUp));
                jaquotOther.transform.localScale = jaquotOther.transform.localScale / scaleRatioBigBallToJaquotBoule;

                jaquotOther.AddComponent<DestroyLinked>().linked = other.gameObject;
                other.collider.gameObject.AddComponent<DestroyLinked>().linked = jaquotOther;
            }

            // Sticking to Big Ball
            {
                Destroy(other.gameObject.GetComponent<Rigidbody>());
                other.transform.SetParent(transform);
                other.gameObject.layer = gameObject.layer;

                Vector3[] cubeCorners = new Vector3[]
                {
                new Vector3( 1,  1,  1),
                new Vector3( 1,  1, -1),
                new Vector3( 1, -1,  1),
                new Vector3( 1, -1, -1),
                new Vector3(-1,  1,  1),
                new Vector3(-1,  1, -1),
                new Vector3(-1, -1,  1),
                new Vector3(-1, -1, -1),
                };

                float maxRadius = 0f;
                foreach (Vector3 cubeCorner in cubeCorners)
                {
                    Vector3 closestPoint = other.collider.ClosestPoint(1000f * cubeCorner);
                    float distToSmallBall = Vector3.Distance(smallBall.transform.position, closestPoint);
                    if (distToSmallBall > maxRadius)
                    {
                        maxRadius = distToSmallBall;
                    }
                }

                bonGarsToSmallBallJoint.autoConfigureConnectedAnchor = false;
                bonGarsToSmallBallJoint.connectedAnchor = new Vector3(0f, bonGarsToSmallBallJoint.connectedAnchor.y, padding * (-maxRadius) / smallBall.transform.localScale.y);
            }
        }
    }
}
