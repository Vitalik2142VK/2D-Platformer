using System.Collections;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField, Min(0)] private float _timeDelete = 5;

    private WaitForSeconds _wait;

    private IEnumerator RemoveAfterWhile()
    {
        yield return _wait;

        Destroy(gameObject);
    }

    public override void Remove()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;


        _wait = new WaitForSeconds(_timeDelete);
        StartCoroutine(RemoveAfterWhile());
    }
}
