using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualWheelMovement : MonoBehaviour
{
    [SerializeField] WheelCollider wheelCollider;

    Vector3 wheelPos;
    Quaternion wheelRot;

    void Update()
    {
        wheelCollider.GetWorldPose(out wheelPos, out wheelRot);

        transform.position = wheelPos;
        transform.rotation = wheelRot;
    }
}
