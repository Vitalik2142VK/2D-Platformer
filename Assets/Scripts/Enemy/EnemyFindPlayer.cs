using System;
using UnityEngine;

public class EnemyFindPlayer : MonoBehaviour
{
    [SerializeField] private LayerMask _damageLayerMask;
    [SerializeField, Min(1)] private float _timeUpdatePlayerPosition;

    public event Action<Player, bool> PlayerFound;

    private bool _isPlayingFound = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _isPlayingFound = true;
            PlayerFound?.Invoke(player, _isPlayingFound);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            _isPlayingFound = false;
            PlayerFound?.Invoke(player, _isPlayingFound);
        }
    }
}
