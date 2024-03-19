using TMPro;
using UnityEngine;

public class HealthCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counterOutput;

    private Health _health;
    private float _maxHealth;
    private float _currentHealth;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

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
