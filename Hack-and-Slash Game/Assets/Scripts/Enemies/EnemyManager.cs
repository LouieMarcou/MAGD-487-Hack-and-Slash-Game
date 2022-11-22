using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<Set> sets;
    [SerializeField] private Set currentSet;
    [SerializeField] private List<WaveData> waves;
    [SerializeField] private WaveData currentWave;
    [SerializeField] private TMP_Text waveText;
    private int waveCount = 0;

    private float timer = 2f;
    private bool stop = false;
    private bool isWaveOver = false;

    [SerializeField] private UpgradeManager upgradeManager;

    // Start is called before the first frame update
    void Start()
    {
        currentSet = sets[0];
        currentWave = sets[0].Waves[waveCount];
        Debug.Log(currentWave);
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
            //waveText.gameObject.SetActive(true);
            isWaveOver = true;
            waveCount++;
            upgradeManager.Run();

        }
        if(waveCount > currentSet.Waves.Count)
        {
            Debug.Log("Set is over");
        }
    }

    //Will start the next wave by getting the total enemies of the wave
    //Will find the object pools belonging to each enemy in the wave and allow them to spawn
    private void StartWave()
    {
        //Debug.Log("starting wave " + (waveCount + 1) );
        currentWave.CalculateTotalEnemies();
        
		for(int i = 0; i < currentWave.EnemyAmountForEachType.Count; i++)
		{
			GameObject obj;
        	obj = GameObject.Find((currentWave.EnemyTypes[i].EnemyBasePrefab.name + "ObjectPool"));
        	obj.GetComponent<EnemyObjectPool>().SetCanSpawn(true);
			obj.GetComponent<EnemyObjectPool>().SetAmountOfEnemiesToSpawn(currentWave.EnemyAmountForEachType[i]);	
		
		}
        
    }

    public void ResetTimer()
    {
        timer = 2f;
        stop = false;
        isWaveOver = false;
        currentWave = waves[waveCount];
    }

    public WaveData GetCurrentWave()
    {
        return currentWave;
    }

    private void OnDisable()
    {
        waveCount = 0;
    }
}
