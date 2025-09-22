using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnerRoadCar : Spawner
{
    bool isCoroutineRunning = false;

    public static bool IsSpawning { get; set; } = false;

    void FixedUpdate()
    {
        if (!IsSpawning) return;

        if (!isCoroutineRunning)
            StartCoroutine(SpawnCoroutine());


    }
    
    IEnumerator SpawnCoroutine()
    {
        isCoroutineRunning = true;

        GameObject carRoad = Instantiate(
            prefabsCar[Random.Range(0, prefabsCar.Count)],
            parentCar
            );

        Transform spawnTransform = spawnTransforms[Random.Range(0, spawnTransforms.Count)];
        carRoad.transform.SetPositionAndRotation(spawnTransform.position, spawnTransform.rotation);

        yield return new WaitForSeconds(Random.Range(2.5f, 3f));

        isCoroutineRunning = false;
    }
    
}
