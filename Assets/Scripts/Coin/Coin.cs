using UnityEngine;

public class Coin : MonoBehaviour 
{
    [SerializeField] private Player _player;

    private bool _isTaken = false;

    private void OnEnable()
    {
        _player.CoinIsTaken += Take;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            _isTaken = true;
    }

    private void OnDisable()
    {
        _player.CoinIsTaken -= Take;
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    private void Take()
    {
        if (_isTaken)
            Destroy(gameObject);
    }
}
