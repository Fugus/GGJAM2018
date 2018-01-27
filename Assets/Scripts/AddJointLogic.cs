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
    private LayerMask validLayersForCollision;

    private void OnCollisionEnter(Collision other)
    {
        if (validLayersForCollision.value == (validLayersForCollision.value | (1 << other.gameObject.layer)))
        {
            Debug.LogFormat(gameObject, "Collided with {0}", other.transform.GetHierarchyPath());

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
