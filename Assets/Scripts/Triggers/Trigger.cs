using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public abstract class Trigger : MonoBehaviour
{
    protected CarParking carParking;
    protected CarRoad carRoad;
    protected string carParkingTag = "CarParking";
    protected string carRoadTag = "CarRoad";
    protected string startCarTag = "StartCar";
}
