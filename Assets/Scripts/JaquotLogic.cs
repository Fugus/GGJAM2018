using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class JaquotLogic : MonoBehaviour
    {
        [SerializeField] float m_MovingTurnSpeed = 360;
        [SerializeField] float m_StationaryTurnSpeed = 180;
        [SerializeField] float m_JumpPower = 12f;
        [SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
        [SerializeField] float m_MoveSpeedMultiplier = 1f;
        [SerializeField] float m_GroundCheckDistance = 0.1f;

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

        [SerializeField]
        [Tooltip("")]
        private float MaxSpeed = 1.0f;


        void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            m_CapsuleHeight = m_Capsule.height;
            m_CapsuleCenter = m_Capsule.center;

            //m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            m_OrigGroundCheckDistance = m_GroundCheckDistance;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, lala);
        }

        Vector3 lala;
        public void Move(Vector3 move, bool crouch, bool jump)
        {

            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired
            // direction.
            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);

            Vector3 fakeGravityDirection = GetComponent<JaquotFakeGravity>().fakeGravityDirection;

            Vector3 worldMove = move;
            move = Vector3.ProjectOnPlane(move, fakeGravityDirection);
            lala = move;
            m_TurnAmount = Mathf.Atan2(move.x, move.z);
            m_ForwardAmount = move.z;

            //ApplyExtraTurnRotation();

            // control and velocity handling is different when grounded and airborne:
            //HandleGroundedMovement(crouch, jump);

            //ScaleCapsuleForCrouching(crouch);
            //PreventStandingInLowHeadroom();

            // send input and other state parameters to the animator

            m_Rigidbody.AddRelativeForce(move * m_MoveSpeedMultiplier, ForceMode.VelocityChange);
            if (m_Rigidbody.velocity.magnitude > MaxSpeed)
            {
                m_Rigidbody.velocity = m_Rigidbody.velocity.normalized * MaxSpeed;
                m_Rigidbody.rotation = Quaternion.LookRotation(m_Rigidbody.velocity, -fakeGravityDirection);
            }

            if (jump)
            {
                m_Rigidbody.AddForce(m_JumpPower *  -fakeGravityDirection, ForceMode.Impulse);
            }

            //m_Rigidbody.AddRelativeTorque(Vector3.up * m_TurnAmount * m_MovingTurnSpeed, ForceMode.VelocityChange);

            //UpdateAnimator(move);
        }


        void HandleGroundedMovement(bool crouch, bool jump)
        {
            // check whether conditions are right to allow a jump:
            if (jump && !crouch)
            {
                // jump!
                m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
                ///m_Animator.applyRootMotion = false;
                m_GroundCheckDistance = 0.1f;
            }
        }

    }
}
