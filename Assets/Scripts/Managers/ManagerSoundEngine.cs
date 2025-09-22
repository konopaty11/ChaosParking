using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerSoundEngine : MonoBehaviour
{
    [SerializeField] AudioSource engineSound;

    Rigidbody carRG;
    float minPitch = 0.7f;
    float maxPitch = 1.5f;
    float maxSpeed = 80f;
    float currentSpeed;
    float volume = 1f;

    void Start()
    {
        carRG = GetComponent<Rigidbody>();
    }

    void Update()
    {
        DefineEngineSound();
    }

    void DefineEngineSound()
    {
        currentSpeed = carRG.velocity.magnitude * 3.6f;
        float speedRatio = Mathf.Clamp01(currentSpeed / maxSpeed);

        if (!engineSound.isPlaying) engineSound.Play();
        engineSound.pitch = Mathf.Lerp(minPitch, maxPitch, speedRatio);
        if (speedRatio < 0.2f) speedRatio += 0.2f;
        engineSound.volume = speedRatio * volume;
    }
}
