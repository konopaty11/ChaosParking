using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStop : Trigger
{
    [SerializeField] protected float speedStop;
    protected string stopTag = "StopTag";
    protected Car carStopping;

    void OnTriggerEnter(Collider car)
    {
        if (car.CompareTag(stopTag))
        {
            if (car.transform.parent.CompareTag(carParkingTag))
            {
                carStopping = car.GetComponentInParent<CarParking>();
            }
            else if (car.transform.parent.CompareTag(carRoadTag) || car.transform.parent.CompareTag(startCarTag))
            {
                carStopping = car.GetComponentInParent<CarRoad>();
            }

            if (carStopping.IsTurning) return;

            carStopping.MagnitudeStop = speedStop;
            carStopping.IsBraking = true;
            carStopping.ResumeDrive();

        }
    }
}
