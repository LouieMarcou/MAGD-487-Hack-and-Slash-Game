using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WaveData : ScriptableObject
{
 	public string WaveName;
    public List<EnemyData> EnemyTypes;
    public List<int> EnemyAmountForEachType;
    private int TotalEnemies;
    public int KillCount;
    public bool hasStarted = false;
    public bool isRunning = false;
    public bool isOver = false;

    public void CalculateTotalEnemies()
    {
        foreach(int enemyAmount in EnemyAmountForEachType)
        {
            TotalEnemies += enemyAmount;
        }
    }

    public int GetTotalEnemies()
    {
        return TotalEnemies;
    }

    public bool CheckIfWaveIsOver()
    {
        if (KillCount == TotalEnemies)
            return true;
        else
            return false;
    }

    void OnDisable()
    {
        TotalEnemies = 0;
        KillCount = 0;
        hasStarted = false;
        isRunning = false;
        isOver = false;
    }
}
