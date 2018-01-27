using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    // TODO: Have a unity event that passes in the controller. clinel 2017-12-05.
    public class ColliderUnityEvents : MonoBehaviour
    {
        [SerializeField]
        private LayerMask validLayersForCollision;

        [SerializeField]
        private string[] validTagsForCollision;

        [SerializeField]
        private UnityEvent OnCollisionEnterEvent;
        [SerializeField]
        private UnityEvent OnCollisionStayEvent;
        [SerializeField]
        private UnityEvent OnCollisionExitEvent;

        private bool IsCollisionValid(Collision other)
        {
            if (validTagsForCollision.Length > 0)
            {
                if (!Array.Exists(validTagsForCollision, tag => tag == other.gameObject.tag))
                {
                    return false;
                }
            }

            if (validLayersForCollision.value != (validLayersForCollision.value | (1 << other.gameObject.layer)))
            {
                return false;
            }

            return true;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (IsCollisionValid(other))
            {
                OnCollisionEnterEvent.Invoke();
            }
        }

        private void OnCollisionStay(Collision other)
        {
            if (IsCollisionValid(other))
            {
                OnCollisionStayEvent.Invoke();
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (IsCollisionValid(other))
            {
                OnCollisionExitEvent.Invoke();
            }
        }
    }
}
