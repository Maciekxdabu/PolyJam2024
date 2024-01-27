using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearcher : MonoBehaviour
{
    [Header("Do not change, for debug only:")]
    [SerializeField] private LayerMask enemyLayers = 0;
    [SerializeField] private float capsuleCircleDistance = 5f;

    // ---------- Unity messages

    // ---------- public methods

    public bool GetEnemiesInRange(float radius, out Enemy[] enemies)
    {
        Collider[] hits = Physics.OverlapCapsule(transform.position + Vector3.up * capsuleCircleDistance,
            transform.position - Vector3.up * capsuleCircleDistance,
            radius,
            enemyLayers);

        List<Enemy> enemiesList = new List<Enemy>();
        foreach (Collider hit in hits)
        {
            enemiesList.Add(hit.GetComponent<Enemy>());
        }

        enemies = enemiesList.ToArray();
        return enemies.Length > 0;
    }
}
