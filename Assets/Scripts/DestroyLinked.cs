using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLinked : MonoBehaviour
{
    public GameObject linked;

    private void OnDestroy()
    {
        Destroy(linked);
    }
}
