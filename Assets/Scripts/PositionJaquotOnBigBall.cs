using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionJaquotOnBigBall : MonoBehaviour
{
    [SerializeField]
    private Transform bouleJaquot;

    float scaleRatioBigBallToJaquotBoule = 1f;

    [SerializeField]
    private Transform smallJaquot;
    [SerializeField]
    private Transform Jaquot;

    private void Start()
    {
        float bigBallScale = GetComponent<Collider>().bounds.extents.magnitude;
        float bouleJaquotScale = bouleJaquot.GetComponent<Collider>().bounds.extents.magnitude;
        scaleRatioBigBallToJaquotBoule = bigBallScale / bouleJaquotScale;
        Debug.LogFormat(gameObject, "scaleRatioBigBallToJaquotBoule {0}", scaleRatioBigBallToJaquotBoule);
    }

    void Update()
    {
        Vector3 bigBallToOtherPosition = bouleJaquot.InverseTransformDirection(Jaquot.position - bouleJaquot.position);
        Vector3 bigBallToOtherUp = bouleJaquot.InverseTransformDirection(Jaquot.up);
        Vector3 bigBallToOtherForward = bouleJaquot.InverseTransformDirection(Jaquot.forward);

        smallJaquot.transform.position = transform.position + transform.TransformDirection(bigBallToOtherPosition) * scaleRatioBigBallToJaquotBoule;
        smallJaquot.transform.rotation = Quaternion.LookRotation(transform.TransformDirection(bigBallToOtherForward), transform.TransformDirection(bigBallToOtherUp));
        smallJaquot.transform.localScale = Jaquot.localScale * scaleRatioBigBallToJaquotBoule;
    }
}
