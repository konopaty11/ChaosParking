using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCarParking : Spawner
{
    List<Transform> spawnTransformsNonSerialize = new();
    List<int> indexTransforms = new();

    public int MaxCountCar { get => spawnTransforms.Count; }

    public void SpawnCars()
    {
        for (int i = 0; i < spawnTransforms.Count; i++)
            indexTransforms.Add(i);

        foreach (Transform trans in spawnTransforms)
            spawnTransformsNonSerialize.Add(trans);

        int countSpawns = spawnTransformsNonSerialize.Count;
        for (int i = 0; i < ManagerCars.CountCarsParking; i++)
        {
            GameObject carParking = Instantiate(
                prefabsCar[Random.Range(0, prefabsCar.Count)],
                parentCar);

            int indexTransform = indexTransforms[Random.Range(0, indexTransforms.Count)];
            indexTransforms.Remove(indexTransform);

            Transform carTransform = spawnTransformsNonSerialize[indexTransform];


            Vector3 rotation = new Vector3(0, 90 * Random.Range(0, 4), 0);
            if (indexTransform % 4 == 0 || indexTransform % 4 == 3)
                rotation = new Vector3(0, 90 * Random.Range(0, 2) * 2, 0);

            carTransform.rotation = Quaternion.Euler(rotation);
            carParking.transform.SetPositionAndRotation(carTransform.position, carTransform.rotation);
        }
    }
}
