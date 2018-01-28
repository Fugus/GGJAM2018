using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropLogic : MonoBehaviour
{
    [SerializeField]
    private LayerMask validLayersForCollision;

    private void OnCollisionEnter(Collision other)
    {
        if (validLayersForCollision.value == (validLayersForCollision.value | (1 << other.gameObject.layer)))
        {
            Debug.LogFormat(gameObject, "Other collider: {0}", other.collider.transform.GetHierarchyPath());
            if (other.collider.gameObject.GetComponent<AddJointLogic>() == null)
            {
                other.collider.gameObject.AddComponent<DestroyLinked>().linked = gameObject;
            }
        }
    }
}
