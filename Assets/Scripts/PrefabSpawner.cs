using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public List<Transform> prefabsToSpawn;

    public bool randomDirection = false;
    public float randomDirectionForce = 1f;

    public float spawnDelay = .2f;

    public bool randomRotation;

    public bool destroyAfterDuration = false;
    public float destroyAfterDurationDelay = 0f;

    private IEnumerator Start()
    {
        while (true)
        {
            Transform instance = Instantiate(prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)], transform.position, randomRotation ? Random.rotation : Quaternion.identity);

            if (destroyAfterDuration)
            {
                instance.gameObject.AddComponent<DestroyAfterDuration>().destroyDuration = destroyAfterDurationDelay;
            }

            if (randomRotation)
            {
                instance.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * randomDirectionForce, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

}
