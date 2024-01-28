using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Wave data")]
    [SerializeField] private LevelSO levelData = null;
    [SerializeField] private float spawnDelay = 1.0f;

    [Header("references")]
    [SerializeField] GameObject enemyPrefab = null;
    [SerializeField] SplineContainer splineCon = null;

    private bool waveRunning = false;
    private int currentWave = 0;

    private bool startNextWave = false;
    private int enemyBlocksRunning = 0;

    //value change events
    public delegate void Deg();
    public Deg onHUDvalueChanged;

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
        //StartCoroutine(SpawnEnemyLoop());
        StartCoroutine(DoLevel());
    }

    // ---------- public methods

    public void StartNextWave_Button()
    {
        if (waveRunning == false)
        {
            startNextWave = true;
        }
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

    public int GetMaxWaves()
    {
        return levelData.waves.Length;
    }

    // ---------- IEnumerators

    #region IEnumerators

    private IEnumerator SpawnEnemyLoop()
    {
        while (waveRunning)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnEnemy();
        }
    }

    //one for a given run
    private IEnumerator DoLevel()
    {
        //spawn waves
        for (int i=0; i<levelData.waves.Length; i++)
        {
            currentWave = i + 1;
            if (onHUDvalueChanged != null)
                onHUDvalueChanged();

            //allow user to start the next wave
            yield return new WaitUntil(() => startNextWave);
            startNextWave = false;

            waveRunning = true;
            StartCoroutine(DoWave(levelData.waves[i]));
            yield return new WaitWhile(() => waveRunning);
        }

        yield return new WaitUntil(() => Enemy.AllDead());

        HUD.Instance.OnGameEnd(true);
    }

    //one at a time
    private IEnumerator DoWave(LevelSO.Wave waveData)
    {
        enemyBlocksRunning = 0;

        //spawn Enemy blocks until they last
        for (int i=0; i<waveData.enemies.Length; i++)
        {
            enemyBlocksRunning++;
            StartCoroutine(DoEnemyBlock(waveData.enemies[i]));
            yield return new WaitForSeconds(waveData.enemies[i].delayToNextBlock);
        }

        //check if wave ended
        yield return new WaitWhile(() => enemyBlocksRunning > 0);

        //end wave before finishing Coroutine
        waveRunning = false;
    }

    private IEnumerator DoEnemyBlock(LevelSO.EnemyBlock block)
    {
        //spawn enemies until they last
        for (int i=0; i<block.amount; i++)
        {
            SpawnEnemy(block.enemy);
            yield return new WaitForSeconds(block.delay);
        }
        enemyBlocksRunning--;
    }

    #endregion

    // ---------- private methods

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, splineCon.EvaluatePosition(0f), Quaternion.identity).GetComponent<Enemy>().Initialize(splineCon);
    }

    private void SpawnEnemy(GameObject _enemyPrefab)
    {
        Instantiate(_enemyPrefab, splineCon.EvaluatePosition(0f), Quaternion.identity).GetComponent<Enemy>().Initialize(splineCon);
    }
}
