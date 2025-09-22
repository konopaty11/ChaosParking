using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class TriggerStopForTurn : TriggerStop
{
    [SerializeField] TargetPosition targetPos;

    void OnTriggerEnter(Collider car)
    {
        if (car.CompareTag(stopTag))
        {
            bool isStartCar = car.transform.parent.CompareTag(startCarTag);

            if (car.transform.parent.CompareTag(carParkingTag))
            {
                carParking = car.GetComponentInParent<CarParking>();
                if (carParking.Direction == Direction.Back) return;

                carStopping = carParking;
            }
            else if (car.transform.parent.CompareTag(carRoadTag) || isStartCar)
            {
                carStopping = car.GetComponentInParent<CarRoad>();
            }

            if (carStopping.TargetPos != targetPos)
                return;

            carStopping.MagnitudeStop = speedStop;
            carStopping.IsBraking = true;
            carStopping.ResumeDrive();
        }
    }
}
