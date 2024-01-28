using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 1f;
    [SerializeField] private int piercing = 1;

    //normal shooting mode
    private Vector3 direction = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;

    //other
    private Rigidbody rb = null;

    // ---------- Unity messages

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.OnHit(damage);
            piercing--;
            if (piercing <= 0)
                Destroy(gameObject);
        }
    }

    // ---------- public methods

    public void Initialize(Transform target, float distance)
    {
        direction = (target.position - transform.position).normalized;
        endPosition = transform.position + direction * distance;
        float tweenTime = (endPosition - transform.position).magnitude / speed;

        //normal
        rb.DOMove(endPosition, tweenTime).SetLink(gameObject, LinkBehaviour.KillOnDestroy).OnComplete(Die).SetEase(Ease.Linear);
    }

    // ---------- private methods

    private void Die()
    {
        Destroy(gameObject);
    }
}
