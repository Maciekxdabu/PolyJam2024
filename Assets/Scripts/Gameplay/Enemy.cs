using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private int damage = 1;
    [SerializeField] private int moneyDropped = 1;

    private SplineContainer spline = null;
    private float splineLength = 0f;
    private Rigidbody rb = null;

    private float timeAlive = 0f;

    //for value assignment
    private float tempFloat = 0f;

    // ---------- Unity messages

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        timeAlive = 0f;
    }

    private void FixedUpdate()
    {
        timeAlive += Time.fixedDeltaTime;
        tempFloat = (speed * timeAlive) / splineLength;
        if (tempFloat > 1.0f)
            OnReachedEnd();
        else
        {
            rb.position = spline.EvaluatePosition(tempFloat);
            rb.rotation = Quaternion.LookRotation(spline.EvaluateTangent(tempFloat), spline.EvaluateUpVector(tempFloat));
        }
    }

    // ---------- public methods

    public void Initialize(SplineContainer _spline)
    {
        spline = _spline;
        splineLength = spline.CalculateLength();
        gameObject.SetActive(true);
    }

    public void OnHit()
    {
        PlayerData.Instance.GiveMoney(moneyDropped);
        Destroy(gameObject);
    }

    // ---------- private methods

    private void OnReachedEnd()
    {
        PlayerData.Instance.TakeDamage(damage);
        Destroy(gameObject);
    }
}
