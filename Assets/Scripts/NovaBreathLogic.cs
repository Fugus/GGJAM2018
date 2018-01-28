using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaBreathLogic : MonoBehaviour
{

    [SerializeField]
    private LayerMask validLayersForCollision;


    private void OnTriggerEnter(Collider other)
    {
        if (validLayersForCollision.value == (validLayersForCollision.value | (1 << other.gameObject.layer)))
        {
            Debug.LogFormat(gameObject, "Novabreath Collided with {0}", other.transform.GetHierarchyPath());

            Destroy(other.gameObject);
        }
    }
}
