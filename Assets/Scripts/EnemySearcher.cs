using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearcher : MonoBehaviour
{
    [Header("Do not change, for debug only:")]
    [SerializeField] private List<Enemy> enemiesInRange = new List<Enemy>();

    // ---------- Unity messages

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
            enemiesInRange.Add(enemy);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
            enemiesInRange.Remove(enemy);
    }

    // ---------- public methods

    public bool TryEnemiesFound(out Enemy[] enemies)
    {
        enemies = enemiesInRange.ToArray();
        return enemies.Length > 0;
    }
}
