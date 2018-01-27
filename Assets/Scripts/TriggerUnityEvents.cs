using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    // TODO: Have a unity event that passes in the controller. clinel 2017-12-05.
    public class TriggerUnityEvents : MonoBehaviour
    {
        [SerializeField]
        private LayerMask validLayersForCollision;

        [SerializeField]
        private string[] validTagsForCollision;

        [SerializeField]
        private UnityEvent OnTriggerEnterEvent;
        [SerializeField]
        private UnityEvent OnTriggerStayEvent;
        [SerializeField]
        private UnityEvent OnTriggerExitEvent;

        private bool IsCollisionValid(Collider other)
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

        private void OnTriggerEnter(Collider other)
        {

				if (IsCollisionValid(other))
            {
                OnTriggerEnterEvent.Invoke();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (IsCollisionValid(other))
            {
                OnTriggerStayEvent.Invoke();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsCollisionValid(other))
            {
                OnTriggerExitEvent.Invoke();
            }
        }

    }
}