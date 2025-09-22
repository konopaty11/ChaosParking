using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class TriggerTurn : Trigger
{
    [SerializeField] protected TargetPosition targetPosition;
    [SerializeField] protected float steerAngle;
    [SerializeField] protected float targetAngleY;
}
