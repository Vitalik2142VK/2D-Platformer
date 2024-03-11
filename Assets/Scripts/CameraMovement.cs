using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _objectObservation;

    void Update()
    {
        transform.position = _objectObservation.position;
    }
}
