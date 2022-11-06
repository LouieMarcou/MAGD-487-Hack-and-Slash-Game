using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private WaveData waveData;

    private float timer = 2f;
    private bool stop = false;
    private bool isWaveOver = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer<0 && stop == false)
        {
            StartWaveOne();
            stop = true;
        }
    }

    private void StartWaveOne()
    {
        Debug.Log("starting wave one");
        waveData.CalculateTotalEnemies();
        GameObject obj;
        obj = GameObject.Find((waveData.EnemyTypes[0].EnemyBasePrefab.name + "ObjectPool"));
        obj.GetComponent<EnemyObjectPool>().SetCanSpawn(true);
        obj.GetComponent<EnemyObjectPool>().SetAmountOfEnemiesToSpawn(waveData.GetTotalEnemies());
    }
}
