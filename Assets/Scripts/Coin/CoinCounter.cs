using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _counterOutput;

    private int _countCoins = 0;

    private void OnEnable()
    {
        _player.CoinTaken += OnUpdateCount;
    }

    private void Start()
    {
        UpdateView();
    }

    private void OnDisable()
    {
        _player.CoinTaken -= OnUpdateCount;
    }

    private void AddCoinToCounter()
    {
        _countCoins++;
    }

    private void UpdateView()
    {
        _counterOutput.text = _countCoins.ToString();
    }

    private void OnUpdateCount()
    {
        AddCoinToCounter();
        UpdateView();
    }
}
