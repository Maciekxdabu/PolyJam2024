using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private static int enemiesAlive = 0;

    [SerializeField] private int hp = 1;
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
        enemiesAlive++;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        timeAlive = 0f;
    }

    private void OnDestroy()
    {
        enemiesAlive--;
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

    // ---------- public static methods

    public static bool AllDead()
    {
        return enemiesAlive <= 0;
    }

    // ---------- public methods

    public void Initialize(SplineContainer _spline)
    {
        spline = _spline;
        splineLength = spline.CalculateLength();
        gameObject.SetActive(true);
    }

    public void OnHit(int amount)
    {
        hp -= amount;
        if (hp <= 0)//die
        {
            AudioManager.Instance.PlayDie();
            PlayerData.Instance.GiveMoney(moneyDropped);
            Destroy(gameObject);
        }
    }

    // ---------- private methods

    private void OnReachedEnd()
    {
        PlayerData.Instance.TakeDamage(damage);
        Destroy(gameObject);
    }
}
