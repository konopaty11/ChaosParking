using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class TriggerTrack : TriggerTurn
{
    void OnTriggerEnter(Collider car)
    {
        if (car.CompareTag(carParkingTag))
        {
            carParking = car.GetComponent<CarParking>();

            if (carParking.IsTurnOnRoad && carParking.TargetPos != targetPosition)
                return;

            carParking.IsTurning = true;
            carParking.SteerAngle = steerAngle;
            carParking.TargetAngleY = targetAngleY;
            carParking.isLowPower = false;
        }
        else if (car.CompareTag(carRoadTag) || car.CompareTag(startCarTag))
        {
            carRoad = car.GetComponent<CarRoad>();

            if (carRoad.TargetPos != targetPosition)
                return;

            carRoad.IsTurning = true;
            carRoad.SteerAngle = steerAngle;
            carRoad.TargetAngleY = targetAngleY;
            carRoad.isLowPower = false;
        }
    }
}
