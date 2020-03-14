using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenSpawner : MonoBehaviour
{
    [SerializeField]
    List<Citizen> citizenPrefabs;

    [SerializeField] float spawnSecondsInterval;

    [SerializeField] float spawnSecondsSaltRandomizer;

    private Citizen citizenSpawned;

    private void Start()
    {
        if (Random.Range(0.0f, 1.0f) > 0.5f) SpawnRandomCitizen();
        StartCoroutine(SpawnCitizen());
    }

    IEnumerator SpawnCitizen()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnSecondsInterval + Random.Range(0, spawnSecondsSaltRandomizer));
            if(citizenSpawned == null)
            {
                SpawnRandomCitizen();
            }
        }
        
    }

    private void SpawnRandomCitizen()
    {
        int randomCitizenIndex = Random.Range(0, citizenPrefabs.Count);

        citizenSpawned = Instantiate(citizenPrefabs[randomCitizenIndex], transform.position, Quaternion.identity);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color32(255, 0, 255, 190);
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}
