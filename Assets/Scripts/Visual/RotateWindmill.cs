using UnityEngine;

public class RotateWindmill : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 180f;

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
