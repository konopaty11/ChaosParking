using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class TriggerStartCar : Trigger
{
    void OnTriggerEnter(Collider car)
    {
        if (car.CompareTag(startCarTag))
        {
            carRoad = car.GetComponent<CarRoad>();

            if (carRoad.TargetPos == TargetPosition.Position_1)
                carRoad.TargetPos = TargetPosition.Position_2;
            else
                carRoad.TargetPos = TargetPosition.Position_1;
        }
    }
}