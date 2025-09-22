using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Car : MonoBehaviour
{
    [SerializeField] protected float enginePower;
    [SerializeField] protected List<WheelCollider> wheelFColiders;
    [SerializeField] protected List<WheelCollider> wheelRColiders;
    [SerializeField] protected AudioSource crashSound;

    [SerializeField] ParticleSystem sparksPrefab;

    protected Rigidbody rg;
    protected float brakeForce = 2500f;
    protected Coroutine driveCoroutine;
    protected float lowEnginePower;
    protected float highEnginePower;
    protected bool isDrive = false;
    protected bool isStopping = false;
    List<WheelCollider> wheels;
    float maxCollisionForce = 4000f;
    float particlesFactor = 50f;
    float startTime = 0f;

    public bool IsTurning { get; set; } = false;
    public float SteerAngle { get; set; }
    public Direction Direction { get; set; } = Direction.None;
    public TargetPosition TargetPos { get; set; }
    public float TargetAngleY { get; set; }
    public bool IsBraking { get; set; } = false;
    public bool isLowPower { get; set; }
    public float MagnitudeStop { get; set; } = 0.1f;

    void Awake()
    {
        TargetPos = (TargetPosition)UnityEngine.Random.Range(0, 2);
        rg = GetComponent<Rigidbody>();

        wheels = wheelFColiders.Concat(wheelRColiders).ToList();
    }

    protected bool Turn()
    {
        if (rg.constraints == RigidbodyConstraints.FreezeRotationY)
            rg.constraints = RigidbodyConstraints.None;

        if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, TargetAngleY)) > 1)
        {
            foreach (WheelCollider wheel in wheelFColiders)
                wheel.steerAngle = SteerAngle;
        }
        else
        {
            foreach (WheelCollider wheel in wheelFColiders)
                wheel.steerAngle = 0;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, TargetAngleY, transform.eulerAngles.z);
            IsTurning = false;

            return true;
        }
        return false;
    }

    public void LaneKeeping()
    {
        float deltaAngle = Mathf.DeltaAngle(transform.eulerAngles.y, TargetAngleY);
        if (Direction == Direction.Back)
            deltaAngle = -deltaAngle;

        if (deltaAngle > 0.2f)
        {
            foreach (WheelCollider wheel in wheelFColiders)
                wheel.steerAngle = 5;
        }
        else if (deltaAngle < -0.2f)
        {
            foreach (WheelCollider wheel in wheelFColiders)
                wheel.steerAngle = -5;
        }
        else
        {
            foreach (WheelCollider wheel in wheelFColiders)
                wheel.steerAngle = 0;
        }

    }

    protected float GetNormalizeAngle(float angle)
    {
        float normalizedAngle = angle % 360f;
        if (normalizedAngle < 0) normalizedAngle += 360f;

        return normalizedAngle;
    }

    protected void Drive()
    {
        foreach (WheelCollider wheel in wheels)
        {
            if (isLowPower) wheel.motorTorque = enginePower / 3.5f;
            else wheel.motorTorque = enginePower;
        }
    }

    protected bool StopCar()
    {
        if (rg.velocity.magnitude > MagnitudeStop)
        {
            if (isDrive)
            {
                isDrive = false;
                DriveOff();
            }

            foreach (WheelCollider wheel in wheelFColiders)
                wheel.brakeTorque = brakeForce;

            foreach (WheelCollider wheel in wheelRColiders)
                wheel.brakeTorque = brakeForce;
        }
        else
        {
            foreach (WheelCollider wheel in wheelFColiders)
                wheel.brakeTorque = 0;

            foreach (WheelCollider wheel in wheelRColiders)
                wheel.brakeTorque = 0;

            IsBraking = false;
            return true;
        }

        return false;
    }

    public void ResumeDrive()
    {
        isStopping = true;
        driveCoroutine = StartCoroutine(DriveTrue());
    }

    IEnumerator DriveTrue()
    {
        yield return new WaitForSeconds(2f);
        isDrive = true;
        isStopping = false;
    }

    void DriveOff()
    {
        foreach (WheelCollider wheel in wheelFColiders)
            wheel.motorTorque = 0;

        foreach (WheelCollider wheel in wheelRColiders)
            wheel.motorTorque = 0;
    }

    public virtual void OnCollisionEnter(Collision car)
    {
        if (Time.time - startTime < 1f) return;

        crashSound.Play();

        float collisionForce = car.impulse.magnitude;
        float forceRatio = Mathf.Clamp01(collisionForce / maxCollisionForce);
        int particlesCount = (int)(forceRatio * particlesFactor);

        ContactPoint contact = car.contacts[0];
        ParticleSystem sparks = Instantiate(sparksPrefab, contact.point, Quaternion.LookRotation(contact.normal));
        sparks.Emit(particlesCount);
        Destroy(sparks.gameObject, 0.5f);

        startTime = Time.time;
    }

}
