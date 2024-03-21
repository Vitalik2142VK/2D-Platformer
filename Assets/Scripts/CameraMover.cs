using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private void Update()
    {
        transform.position = _player.position;
    }
}
