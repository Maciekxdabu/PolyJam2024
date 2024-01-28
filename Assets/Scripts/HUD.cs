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
    [Space]
    [SerializeField] private Tower tower1 = null;
    [SerializeField] private TMP_Text tower1Cost = null;
    [SerializeField] private Tower tower2 = null;
    [SerializeField] private TMP_Text tower2Cost = null;
    [SerializeField] private Tower tower3 = null;
    [SerializeField] private TMP_Text tower3Cost = null;
    [SerializeField] private Tower tower4 = null;
    [SerializeField] private TMP_Text tower4Cost = null;
    [Header("Other References")]
    [SerializeField] private Canvas gameOverCanvas = null;
    [SerializeField] private TMP_Text gameOverText = null;
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

        tower1Cost.text = tower1.GetCost().ToString();
        tower2Cost.text = tower2.GetCost().ToString();
        tower3Cost.text = tower3.GetCost().ToString();
        tower4Cost.text = tower4.GetCost().ToString();

        UpdateHudValues();
    }

    private void OnDestroy()
    {
        PlayerData.Instance.onHUDvalueChanged -= UpdateHudValues;
        enemySpawner.onHUDvalueChanged -= UpdateHudValues;
    }

    // ---------- public methods

    public void OnGameEnd(bool won)
    {
        if (gameOverCanvas.gameObject.activeSelf == true)//game has already ended
        {
            return;
        }

        gameOverCanvas.gameObject.SetActive(true);

        if (won)
        {
            gameOverText.text = "You Won!";
        }
        else
        {
            gameOverText.text = "You Lost!";
        }
    }

    // ---------- updaters

    private void UpdateHudValues()
    {
        hpText.text = PlayerData.Instance.hp.ToString();
        moneyText.text = PlayerData.Instance.money.ToString();
        waveText.text = enemySpawner.GetCurrentWave() + "/" + enemySpawner.GetMaxWaves();
    }
}
