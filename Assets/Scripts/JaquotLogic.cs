using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class JaquotLogic : MonoBehaviour
    {
        [SerializeField] float m_MovingTurnSpeed = 360;
        [SerializeField] float m_StationaryTurnSpeed = 180;
        [SerializeField] float m_DashPower = 12f;
        [SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
        [SerializeField] float m_MoveSpeedMultiplier = 1f;
        [SerializeField] float m_GroundCheckDistance = 0.1f;
        [SerializeField]
        private GameObject NovaBreath;

        [SerializeField]
        private float NovaBreathCoolDown = 3f;


        Rigidbody m_Rigidbody;
        float m_OrigGroundCheckDistance;
        const float k_Half = 0.5f;
        float m_TurnAmount;
        float m_ForwardAmount;
        Vector3 m_GroundNormal;
        float m_CapsuleHeight;
        Vector3 m_CapsuleCenter;
        CapsuleCollider m_Capsule;
        bool m_Crouching;

        bool canDoNovaBreath = true;

        [SerializeField]
        [Tooltip("")]
        private float MaxSpeed = 1.0f;

        void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            m_CapsuleHeight = m_Capsule.height;
            m_CapsuleCenter = m_Capsule.center;

            NovaBreath.SetActive(false);

            //m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            m_OrigGroundCheckDistance = m_GroundCheckDistance;
        }

        IEnumerator DoNovaBreathCoroutine()
        {
            canDoNovaBreath = false;

            m_Rigidbody.AddForce(m_DashPower * m_Rigidbody.transform.forward, ForceMode.Impulse);

            NovaBreath.SetActive(true);

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / NovaBreathCoolDown;
                yield return null;
            }

            NovaBreath.SetActive(false);

            canDoNovaBreath = true;
        }

        public void Move(Vector3 move, bool crouch, bool jump)
        {
            if (jump && canDoNovaBreath)
            {
                Debug.LogFormat(gameObject, "JUMP!");
                StartCoroutine(DoNovaBreathCoroutine());
            }

            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired
            // direction.
            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);

            Vector3 fakeGravityDirection = GetComponent<JaquotFakeGravity>().fakeGravityDirection;

            Vector3 localMove = move;
            move = Vector3.ProjectOnPlane(move, -fakeGravityDirection);
            m_TurnAmount = Mathf.Atan2(move.x, move.z);
            m_ForwardAmount = move.z;

            //ApplyExtraTurnRotation();

            // control and velocity handling is different when grounded and airborne:
            //HandleGroundedMovement(crouch, jump);

            //ScaleCapsuleForCrouching(crouch);
            //PreventStandingInLowHeadroom();

            // send input and other state parameters to the animator

            m_Rigidbody.AddRelativeForce(localMove * m_MoveSpeedMultiplier, ForceMode.Force);
            if (m_Rigidbody.velocity.magnitude > MaxSpeed)
            {
                m_Rigidbody.AddForce(-m_Rigidbody.velocity, ForceMode.Force);
            }

            if (m_Rigidbody.velocity.magnitude > 0.1f)
            {
                m_Rigidbody.rotation = Quaternion.LookRotation(m_Rigidbody.velocity.normalized, -fakeGravityDirection);

                /*                 Quaternion fromForwardToVelocity = Quaternion.FromToRotation(transform.forward, m_Rigidbody.velocity.normalized);
                                Vector3 axis;
                                float turn;
                                fromForwardToVelocity.ToAngleAxis(out turn, out axis);
                                m_Rigidbody.AddRelativeTorque(transform.InverseTransformDirection(axis) * turn * m_MovingTurnSpeed, ForceMode.Force); */
                //m_Rigidbody.rotation = Quaternion.identity;// Quaternion.LookRotation(m_Rigidbody.velocity.normalized, -fakeGravityDirection);
            }
        }
    }
}
