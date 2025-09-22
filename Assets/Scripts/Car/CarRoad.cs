using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRoad : Car
{
    void Start()
    {
        isDrive = true;
        Direction = Assets.Scripts.Direction.Forward;

        TargetAngleY = transform.eulerAngles.y;
    }

    void FixedUpdate()
    {
        if (IsTurning)
        {
            Turn();
        }
        else
        {
            LaneKeeping();
        }

        if (isDrive)
        {
            Drive();
        }

        if (IsBraking)
            StopCar();
        else if (!isStopping)
            isDrive = true;

    }
}
