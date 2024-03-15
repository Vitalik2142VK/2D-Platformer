using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Text _counterOutput;

    private float _maxHealth;
    private float _currentHealth;

    private void OnEnable()
    {
        _health.HealthChange += UpdateCount;
    }

    private void Start()
    {
        if (TryGetComponent(out Health health))
        {
            _maxHealth = health.MaxCountHealth;
            _currentHealth = _maxHealth;
        }

        OutputCountHealth();
    }

    private void OnDisable()
    {
        _health.HealthChange -= UpdateCount;
    }

    private void ChangeCountHealth(float currentHelath)
    {
        _currentHealth = currentHelath;
    }

    private void OutputCountHealth()
    {
        _counterOutput.text = $"{_currentHealth} / {_maxHealth}";
    }

    private void UpdateCount(float currentHelath)
    {
        ChangeCountHealth(currentHelath);
        OutputCountHealth();
    }
}
