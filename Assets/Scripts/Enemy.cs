using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private int damage = 1;

    private SplineContainer spline = null;
    private float splineLength = 0f;

    private float timeAlive = 0f;

    //for value assignment
    private float tempFloat = 0f;

    // ---------- Unity messages

    private void Start()
    {
        timeAlive = 0f;
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;
        tempFloat = (speed * timeAlive) / splineLength;
        if (tempFloat > 1.0f)
            OnReachedEnd();
        else
            transform.position = spline.EvaluatePosition(tempFloat);
    }

    // ---------- public methods

    public void Initialize(SplineContainer _spline)
    {
        spline = _spline;
        splineLength = spline.CalculateLength();
        gameObject.SetActive(true);
    }

    // ---------- private methods

    private void OnReachedEnd()
    {
        PlayerData.Instance.TakeDamage(damage);
        Destroy(gameObject);
    }
}
