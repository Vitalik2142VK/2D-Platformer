using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private float _damage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _character.TakeDamage(_damage);

            Debug.Log($"Персонажу {_character.name} нанесено {_damage}");
        }
    }
}
