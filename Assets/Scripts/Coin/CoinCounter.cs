using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _counterOutput;

    private int _countCoins = 0;

    private void OnEnable()
    {
        _player.CoinIsTaken += UpdateCount;
    }

    private void Start()
    {
        OutputCountCoins();
    }

    private void OnDisable()
    {
        _player.CoinIsTaken -= UpdateCount;
    }

    private void AddOneCoinToCounter()
    {
        _countCoins++;
    }

    private void OutputCountCoins()
    {
        _counterOutput.text = _countCoins.ToString();
    }

    private void UpdateCount()
    {
        AddOneCoinToCounter();
        OutputCountCoins();
    }
}
