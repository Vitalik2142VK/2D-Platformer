using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private const float RotateY = 180.0f;

    [SerializeField] private Health _health;
    [SerializeField, Min(20.0f)] private float _speedChangeHealth = 10.0f;
    [SerializeField] private bool _isSmoothnessEnabled = true;

    private Slider _healthBar;
    private float _targetHealth;

    private Camera _camera;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _health.HealthChange += UpdateValueHealthBar;
    }

    private void Start()
    {
        _targetHealth = _health.MaxCountHealth;

        _healthBar.maxValue = _targetHealth;
        _healthBar.value = _targetHealth;
    }

    private void Update()
    {
        if (_isSmoothnessEnabled)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, _targetHealth, _speedChangeHealth * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, transform.position.y, _camera.transform.position.z));
        transform.Rotate(0, RotateY, 0);
    }

    private void OnDisable()
    {
        _health.HealthChange -= UpdateValueHealthBar;
    }

    private void UpdateValueHealthBar(float health)
    {
        if (_isSmoothnessEnabled == false)
        {
            _healthBar.value = health;
        }

        _targetHealth = health;
    }
}
