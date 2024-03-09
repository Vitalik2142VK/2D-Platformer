using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _objectObservation;

    void Update()
    {
        transform.position = _objectObservation.position;
    }
}
