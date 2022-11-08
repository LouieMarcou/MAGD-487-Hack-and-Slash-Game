using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private WaveData waveData;
    [SerializeField] private WaveData currentWave;
    [SerializeField] private TMP_Text waveText;

    private float timer = 2f;
    private bool stop = false;
    private bool isWaveOver = false;

    [SerializeField] private UpgradeManager upgradeManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0 && stop == false)
        {
            StartWave();
            stop = true;
        }
        else if(stop && currentWave.KillCount == currentWave.GetTotalEnemies() && isWaveOver == false)
        {
            waveText.gameObject.SetActive(true);
            isWaveOver = true;
            upgradeManager.Run();

        }
    }

    private void StartWave()
    {
        Debug.Log("starting wave one");
        waveData.CalculateTotalEnemies();
        GameObject obj;
        obj = GameObject.Find((currentWave.EnemyTypes[0].EnemyBasePrefab.name + "ObjectPool"));
        obj.GetComponent<EnemyObjectPool>().SetCanSpawn(true);
        obj.GetComponent<EnemyObjectPool>().SetAmountOfEnemiesToSpawn(waveData.GetTotalEnemies());
    }

    public WaveData GetCurrentWave()
    {
        return currentWave;
    }
}
