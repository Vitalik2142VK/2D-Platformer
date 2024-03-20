using UnityEngine;

public class SmoothHealthBar : HealthBar
{
    [SerializeField, Min(20.0f)] private float _speedChangeHealth = 10.0f;

    private float _targetHealth;
    private bool _isSmoothnessEnabled = false;

    private new void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _targetHealth = Health.MaxCountHealth;

        HealthScale.maxValue = _targetHealth;
        HealthScale.value = _targetHealth;
    }

    private void Update()
    {
        if (_isSmoothnessEnabled)
        {
            HealthScale.value = Mathf.MoveTowards(HealthScale.value, _targetHealth, _speedChangeHealth * Time.deltaTime);

            if (HealthScale.value == _targetHealth) 
            {
                _isSmoothnessEnabled = false;
            }
        }
    }

    protected override void UpdateValueHealthBar(float health)
    {
        _targetHealth = health;
        _isSmoothnessEnabled = true;
    }
}
