using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOtherCars : Spawner
{
    List<Transform> spawnTransformsNonSerialize = new();

    void Start()
    {
        foreach (Transform trans in spawnTransforms)
            spawnTransformsNonSerialize.Add(trans);

        int countCars = Random.Range(2, spawnTransformsNonSerialize.Count);
        for (int i = 0; i < countCars; i++)
        {
            GameObject car = Instantiate(prefabsCar[Random.Range(0, prefabsCar.Count)], parentCar);

            int indexTransform = Random.Range(0, spawnTransformsNonSerialize.Count);
            Transform carTransform = spawnTransformsNonSerialize[indexTransform];
            spawnTransformsNonSerialize.RemoveAt(indexTransform);
            car.transform.SetPositionAndRotation(carTransform.position, carTransform.rotation);
        }
    }


}
