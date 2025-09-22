using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoad : TriggerTurn
{
    void OnTriggerEnter(Collider car)
    {
        if (car.CompareTag(carParkingTag))
        {
            carParking = car.GetComponent<CarParking>();
            if (carParking.TargetPos != targetPosition) return;

            carParking.IsTurning = true;
            carParking.SteerAngle = steerAngle;
            carParking.TargetAngleY = targetAngleY;
            carParking.isLowPower = true;
            carParking.IsTurnOnRoad = true;
        }
        else if (car.CompareTag(carRoadTag) || car.CompareTag(startCarTag))
        {
            carRoad = car.GetComponent<CarRoad>();
            if (carRoad.TargetPos != targetPosition) return;
           
            carRoad.IsTurning = true;
            carRoad.SteerAngle = steerAngle;
            carRoad.TargetAngleY = targetAngleY;
            carRoad.isLowPower = true;
        }
    }
}
