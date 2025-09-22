using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CarParking : Car, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject prefabArrow;

    GameObject arrow;
    Vector3 firstPos;
    float enginePowerForward;
    float enginePowerBack;
    bool isTurned = false;
    bool isLeave = false;
    float minSwipeDistance = 0.1f;
    ManagerMenu managerMenu;

    public bool IsTurnOnRoad { get; set; } = false;

    void Start()
    {
        managerMenu = FindFirstObjectByType<ManagerMenu>();

        enginePowerForward = enginePower;
        enginePowerBack = -enginePower / 2;
        isLowPower = true;

        TargetAngleY = transform.eulerAngles.y;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Direction != Direction.None || managerMenu.IsLost) return;
        firstPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Direction != Direction.None || managerMenu.IsLost) return;
        DetermineDirectionMovement(eventData.position);

        if (Direction != Direction.None) 
            isDrive = true;
    }

    void FixedUpdate()
    {
        if (IsTurning)
        {
            if (!isLeave)
            {
                Destroy(arrow);
                isLeave = true;
                ManagerCars.CountCarsParking--;
            }

            isTurned = Turn();
        }
        else
            LaneKeeping();

        if (isDrive)
        {
            Drive();
        }

        if ((Direction == Direction.Back && isTurned) || IsBraking)
        {
            if (StopCar() && Direction == Direction.Back)
            {
                Direction = Direction.Forward;
                enginePower = enginePowerForward;
                isDrive = true;
            }
        }
        else if (!isStopping && Direction != Direction.None)
            isDrive = true;
    }
    
    void DetermineDirectionMovement(Vector3 finishPos)
    {
        int yRot = Convert.ToInt32(transform.eulerAngles.y);
        Vector3 cameraDirection = finishPos - firstPos;
        Vector3 worldDirection = Camera.main.transform.TransformDirection(cameraDirection).normalized;

        if (yRot % 180 == 0)
        {
            if (worldDirection.z > minSwipeDistance && yRot == 0 || worldDirection.z < -minSwipeDistance && yRot == 180)
            {
                Direction = Direction.Forward;
            }
            else if (Mathf.Abs(worldDirection.z) > minSwipeDistance)
            {
                Direction = Direction.Back;
                enginePower = enginePowerBack;
            }
        }
        else
        {
            if (worldDirection.x > minSwipeDistance && yRot == 90 || worldDirection.x < -minSwipeDistance && yRot == 270)
            {
                Direction = Direction.Forward;
            }
            else if (Mathf.Abs(worldDirection.x) > minSwipeDistance)
            {
                Direction = Direction.Back;
                enginePower = enginePowerBack;
            }
        }

        CreateArrowDirection();
    }

    void CreateArrowDirection()
    {
        if (Direction == Direction.None) return;

        arrow = Instantiate(prefabArrow, transform);
        arrow.transform.localPosition = new Vector3(0, 2.2f, 0);

        if (Direction == Direction.Back)
            arrow.transform.localEulerAngles = new Vector3(
                arrow.transform.localEulerAngles.x,
                arrow.transform.localEulerAngles.y + 180,
                arrow.transform.localEulerAngles.z
                );
    }


    public override void OnCollisionEnter(Collision car)
    {
        base.OnCollisionEnter(car);
        if (isLeave) return;

        StopCar();
        managerMenu.LostGame();
    }
}
