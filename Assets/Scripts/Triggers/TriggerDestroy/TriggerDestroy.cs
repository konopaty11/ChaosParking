using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDestroy : Trigger
{
    void OnTriggerEnter(Collider car)
    {
        if (car.CompareTag(carParkingTag) || car.CompareTag(carRoadTag) || car.CompareTag(startCarTag))
        {
            Destroy(car.gameObject);
        }
    }
}
