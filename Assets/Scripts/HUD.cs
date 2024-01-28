using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD Instance { get; private set; }

    [Header("HUD References")]
    [SerializeField] private TMP_Text hpText = null;
    [SerializeField] private TMP_Text moneyText = null;
    [SerializeField] private TMP_Text waveText = null;
    [Header("Other References")]
    [SerializeField] private Canvas gameOverCanvas = null;
    [SerializeField] private EnemySpawner enemySpawner = null;

    // ---------- Unity messages

    private void Awake()
    {
        Instance = this;

        //disable some sub-Canvases
        gameOverCanvas.gameObject.SetActive(false);
    }

    private void Start()
    {
        PlayerData.Instance.onHUDvalueChanged += UpdateHudValues;
        enemySpawner.onHUDvalueChanged += UpdateHudValues;

        UpdateHudValues();
    }

    private void OnDestroy()
    {
        PlayerData.Instance.onHUDvalueChanged -= UpdateHudValues;
        enemySpawner.onHUDvalueChanged -= UpdateHudValues;
    }

    // ---------- public methods

    public void OnGameEnd()
    {
        gameOverCanvas.gameObject.SetActive(true);
    }

    // ---------- updaters

    private void UpdateHudValues()
    {
        hpText.text = PlayerData.Instance.hp.ToString();
        moneyText.text = PlayerData.Instance.money.ToString();
        waveText.text = enemySpawner.GetCurrentWave() + "/" + enemySpawner.GetMaxWaves();
    }
}
