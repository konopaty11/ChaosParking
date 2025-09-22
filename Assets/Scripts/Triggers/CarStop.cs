using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts;

public class CarStop : MonoBehaviour
{
    Car car;
    Car secondCar;
    string carParkingTag = "CarParking";
    string carRoadTag = "CarRoad";
    string startCarTag = "StartCar";
    float speedStop = 0.1f;
    ManagerMenu managerMenu;

    private void Start()
    {
        managerMenu = FindFirstObjectByType<ManagerMenu>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(carParkingTag) || other.CompareTag(carRoadTag) || other.CompareTag(startCarTag))
        {
            CarParking carParking = GetComponentInParent<CarParking>();
            if (carParking != null)
                car = carParking;
            else
                car = GetComponentInParent<CarRoad>();

            if (car.Direction == Direction.Forward)
            {
                car.MagnitudeStop = speedStop;
                car.IsBraking = true;

                secondCar = other.GetComponent<CarParking>();
                if (secondCar == null)
                    secondCar = other.GetComponent<CarRoad>();

                int deltaAngle = Convert.ToInt32(Mathf.Abs(Mathf.DeltaAngle(car.transform.eulerAngles.y, secondCar.transform.eulerAngles.y)));
                if (secondCar.Direction == Direction.Forward && deltaAngle == 180)
                    managerMenu.LostGame();
            }

        }
    }
}
