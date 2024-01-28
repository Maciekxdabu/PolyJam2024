using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "PolyJam 2024/Level")]
public class LevelSO : ScriptableObject
{
    [System.Serializable]
    public class Wave
    {
        public EnemyBlock[] enemies;
    }

    [System.Serializable]
    public class EnemyBlock
    {
        public GameObject enemy = null;
        public int amount = 1;
        public float delay = 1.0f;
        public float delayToNextBlock = 1.0f;
    }

    public Wave[] waves;
}
