using UnityEngine;
using UnityEngine.UI;

public abstract class HealthBar : MonoBehaviour
{
    private const float RotateY = 180.0f;

    [SerializeField] private Health _health;

    protected Slider HealthScale;

    private Camera _camera;

    protected Health Health => _health;

    protected void Awake()
    {
        HealthScale = GetComponent<Slider>();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _health.Change += OnUpdateValue;
    }

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, transform.position.y, _camera.transform.position.z));
        transform.Rotate(0, RotateY, 0);
    }

    private void OnDisable()
    {
        _health.Change -= OnUpdateValue;
    }

    protected abstract void OnUpdateValue(float health);
}
