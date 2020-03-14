using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCrateSpawner : MonoBehaviour
{
    [SerializeField]
    AmmoCrate ammoCratePrefab;

    [SerializeField] float spawnSecondsInterval;

    [SerializeField] float spawnSecondsSaltRandomizer;

    private AmmoCrate crateSpawned;

    private void Start()
    {
        if (Random.Range(0.0f, 1.0f) > 0.5f) SpawnBulletCrate();
        StartCoroutine(SpawnCrates());
    }

    IEnumerator SpawnCrates()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnSecondsInterval + Random.Range(0, spawnSecondsSaltRandomizer));
            if (crateSpawned == null)
            {
                SpawnBulletCrate();
            }
        }

    }

    private void SpawnBulletCrate()
    {
        crateSpawned = Instantiate(ammoCratePrefab, transform.position, Quaternion.identity);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color32(0, 0, 255, 190);
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}
