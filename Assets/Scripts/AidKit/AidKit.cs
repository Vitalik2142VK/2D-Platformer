using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField] private float _restoresHealth;

    public float Use()
    {
        Destroy(gameObject);

        return _restoresHealth;
    }
}
