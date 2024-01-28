using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    [SerializeField] private int maxHP = 20;

    public int money { private set { _money = value; if (onHUDvalueChanged != null) onHUDvalueChanged(); } get { return _money; } }
    [Header("Debug, Readonly")]
    [SerializeField] private int _money = 20;
    public int hp { private set { _hp = value; if (onHUDvalueChanged != null) onHUDvalueChanged(); } get { return _hp; } }
    [SerializeField] private int _hp;

    private bool alive = true;

    //value change events
    public delegate void Deg();
    public Deg onHUDvalueChanged;

    // ---------- Unity messages

    private void Awake()
    {
        Instance = this;

        hp = maxHP;
        alive = true;
    }

    // ---------- public methods

    public void TakeDamage(int damage=1)
    {
        if (alive == false)
            return;

        hp -= damage;

        if (hp <= 0)
            OnDeath();
    }

    public void GiveMoney(int amount=1)
    {
        money += amount;
    }

    // ---------- private methods

    private void OnDeath()
    {
        alive = false;
        HUD.Instance.OnGameEnd();
    }
}
