using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public enum Mode
    {
        normal,
        homing
    }

    
    [SerializeField] private float speed = 1f;
    [SerializeField] private int piercing = 1;
    
    private Mode mode = Mode.normal;

    //homing
    private Transform homingTarget = null;
    private Vector3 positionTarget = Vector3.zero;

    //normal
    private Vector3 direction = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;

    //other
    private Rigidbody rb = null;

    // ---------- Unity messages

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        switch (mode)
        {
            case Mode.normal:
                break;
            case Mode.homing:
                break;
            default:
                Debug.LogWarning("WAR: There is an unknown projectile mode", gameObject);
                break;
        }
    }

    // ---------- public methods

    public void Initialize(Transform target, float distance, Mode _mode=Mode.normal)
    {
        mode = _mode;
        //normal
        direction = (target.position - transform.position).normalized;
        endPosition = transform.position + direction * distance;
        float tweenTime = (endPosition - transform.position).magnitude / speed;
        //homing
        positionTarget = target.position;
        homingTarget = target;

        //normal
        rb.DOMove(endPosition, tweenTime).SetLink(gameObject, LinkBehaviour.KillOnDestroy).OnComplete(Die).SetEase(Ease.Linear);
    }

    // ---------- private methods

    private void Die()
    {
        Destroy(gameObject);
    }
}
