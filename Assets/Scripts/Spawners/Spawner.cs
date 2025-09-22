using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected List<GameObject> prefabsCar;
    [SerializeField] protected List<Transform> spawnTransforms;
    [SerializeField] protected Transform parentCar;
}
