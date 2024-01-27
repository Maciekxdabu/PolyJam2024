using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    [Header("Wave data")]
    [SerializeField] private float spawnDelay = 1.0f;

    [Header("references")]
    [SerializeField] GameObject enemyPrefab = null;
    [SerializeField] SplineContainer splineCon = null;

    [SerializeField] private bool waveRunning = true;

    // ---------- Unity messages

    private void Awake()
    {
        // check values
        if (splineCon == null)
        {
            splineCon = GetComponent<SplineContainer>();
            if (splineCon == null)
            {
                Debug.LogError("There is no spline assigned to the EnemySpawner", gameObject);
                return;
            }
        }
        if (enemyPrefab == null || enemyPrefab.GetComponents<Enemy>() == null)
        {
            Debug.LogError("There is either no Enemy prefab assigned or prefab does not contain an Enemy scirpt");
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemyLoop());
    }

    // ---------- IEnumerators

    private IEnumerator SpawnEnemyLoop()
    {
        while (waveRunning)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnEnemy();
        }
    }

    // ---------- 

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, splineCon.EvaluatePosition(0f), Quaternion.identity).GetComponent<Enemy>().Initialize(splineCon);
    }
}
