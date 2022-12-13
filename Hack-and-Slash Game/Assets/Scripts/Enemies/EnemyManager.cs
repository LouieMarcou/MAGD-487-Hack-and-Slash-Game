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
    public int waveCount = 0;
    private int setCount;

    [SerializeField] private UpgradeManager upgradeManager;

    [SerializeField] private TMP_Text restTimerText;
    private float restTime = 10f;
    public float totalEnmiesKilled;

    public List<EnemyData> Enemies;

    private WaitForSeconds timeToCheckKillCount = new WaitForSeconds(2f);

    //Make state machine???

    // Start is called before the first frame update
    void Start()
    {
        currentSet = sets[0];
        currentWave = sets[0].Waves[waveCount];
        LoadWaves();
        //Debug.Log(currentWave);
        modifyEnemyStats();
    }

    public EnemyManagerState waveState;
    public enum EnemyManagerState
    {
        waveStart,
        waveRunning,
        waveOver,
        waveTransition
    }

    void WaveHandler()
    {
        if(currentWave.hasStarted == false && currentWave.isRunning == false)
        {
            //Debug.Log("Wave can start");
            waveState = EnemyManagerState.waveStart;
        }
        else if(currentWave.hasStarted && currentWave.isRunning)
        {
            //Debug.Log("Wave has started and is running");
            waveState = EnemyManagerState.waveRunning;
        }
        else if (currentWave.isOver)
        {
            //Debug.Log("wave is over");
            waveState = EnemyManagerState.waveOver;
        }
    }

    void HandleWaveState()
    {
        if(waveState == EnemyManagerState.waveStart)
        {
            StartWave();
        }

        else if(waveState == EnemyManagerState.waveRunning)
        {
            //Debug.Log("Wave is running, defeat all enemies");
            if(currentWave.CheckIfWaveIsOver() == false)
            {
                StartCoroutine(CheckCurrentWaveKillCount());
            }
        }

        else if(waveState == EnemyManagerState.waveOver)
        {
            //Debug.Log("All enemies have been defated. Wave is over");
            upgradeManager.Run();
        }
    }

    // Update is called once per frame
    void Update()
    {
        WaveHandler();
        HandleWaveState();
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
        currentWave.hasStarted = true;
        currentWave.isRunning = true;
    }

    IEnumerator CheckCurrentWaveKillCount()
    {
        yield return timeToCheckKillCount;
        //Debug.Log(currentWave.CheckIfWaveIsOver());
        if(currentWave.CheckIfWaveIsOver())
        {
            currentWave.isOver = true;
            currentWave.isRunning = false;
        }
        
    }

    public void ResetTimer()
    {
        //Debug.Log("Reseting timer");
        waveCount++;
        if(setCount < sets.Count)
        {
            //Debug.Log("WaveCout is " + waveCount);
            //Debug.Log("Number of waves is " + waves.Count);
            if(waveCount < waves.Count)
            {
                Debug.Log(waveCount);
                Debug.Log(waves[waveCount]);
                currentWave = waves[waveCount];
            }
            else if(waveCount == waves.Count)
            {
                //Debug.Log("current set is over, moving to the next one");
                setCount++;
                if(setCount == sets.Count)
                {
                    //Debug.Log("Game is over");
                    EndGame();
                    return;
                }
                currentSet = sets[setCount];
                waveCount = 0;
                currentWave = currentSet.Waves[waveCount];
                LoadWaves();
            }
        }
    }

    void LoadWaves()
    {
        waves.Clear();
        for(int i = 0; i < currentSet.Waves.Count; i++)
        {
            waves.Add(currentSet.Waves[i]);
        }
    }
	
	public void modifyEnemyStats()
	{
		foreach(EnemyData enemy in Enemies)
		{
			enemy.stats.health *= currentSet.statModifier;
			enemy.stats.damage *= currentSet.statModifier;
			enemy.stats.armor *= currentSet.statModifier;
			//enemy.stats.speed *= currentSet.statModifier;
		}
	}

    private void EndGame()
    {
        GetComponent<Score>().SetScore();
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
