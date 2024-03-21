using UnityEngine;

public class Damage : MonoBehaviour
{
    private const KeyCode DamageKey = KeyCode.F;

    [SerializeField] private Character _character;
    [SerializeField] private float _damage;

    private void Update()
    {
        if (Input.GetKeyDown(DamageKey))
        {
            _character.TakeDamage(_damage);

            Debug.Log($"��������� {_character.name} �������� {_damage}");
        }
    }
}
