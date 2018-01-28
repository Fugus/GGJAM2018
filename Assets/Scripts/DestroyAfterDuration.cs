using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDuration : MonoBehaviour
{

    public float destroyDuration = 1f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(destroyDuration);
        Destroy(gameObject);
    }
}
