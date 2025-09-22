using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerCamera : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera startVirtualCamera;
    [SerializeField] List<CinemachineVirtualCamera> virtualCamers;

    int index = 0;
    CinemachineBrain cinemachineBrain;

    void Start()
    {
        cinemachineBrain = GetComponent<CinemachineBrain>();
    }

    void Update()
    {
        if (ManagerCars.IsStarted)
        {
            index = 0;

            startVirtualCamera.Priority = 0;
            virtualCamers[index].Priority = 10;

            ManagerCars.IsStarted = false;
        }
    }

    public void ChangeCamera()
    {
        if (cinemachineBrain.IsBlending) return;

        virtualCamers[index].Priority = 0;

        if (++index == virtualCamers.Count)
            index = 0;

        virtualCamers[index].Priority = 10;
    }
}
