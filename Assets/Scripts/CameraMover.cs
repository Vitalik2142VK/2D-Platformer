using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _objectObservation;

    private void Update()
    {
        transform.position = _objectObservation.position;
    }
}
