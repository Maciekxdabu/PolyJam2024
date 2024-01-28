using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float shootDelay = 2.0f;
    [SerializeField] private float range = 2f;
    [SerializeField] private int cost = 5;
    [SerializeField] private int audioIndex = -1;
    [Space]
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private MeshFilter meshFilter = null;
    [SerializeField] private MeshRenderer meshRend = null;
    [SerializeField] private CapsuleCollider rangeTrigger = null;
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private EnemySearcher searcher = null;

    private bool working = true;

    // ---------- Unity messages

    void Start()
    {
        AudioManager.Instance.IncreaseAudio(audioIndex);

        StartCoroutine(BehaviourLoop());
    }

    private void OnValidate()
    {
        if (rangeTrigger != null)
        {
            rangeTrigger.radius = range;
        }
    }

    // ---------- public methods

    public Mesh GetMesh()
    {
        return meshFilter.sharedMesh;
    }

    public int GetMaterialsAmount()
    {
        return meshRend.sharedMaterials.Length;
    }

    public int GetCost()
    {
        return cost;
    }

    // ---------- IEnumerators

    private IEnumerator BehaviourLoop()
    {
        while (working)
        {
            yield return new WaitForSeconds(shootDelay);

            CheckForTarget();
        }
    }

    // ---------- private methods

    private void CheckForTarget()
    {
        if (searcher.GetEnemiesInRange(range, out Enemy[] enemies))
        {
            Shoot(enemies[0]);
        }
    }

    private void Shoot(Enemy enemy)
    {
        AudioManager.Instance.PlayShoot();
        Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity).GetComponent<Projectile>().Initialize(enemy.transform, range);
    }
}
