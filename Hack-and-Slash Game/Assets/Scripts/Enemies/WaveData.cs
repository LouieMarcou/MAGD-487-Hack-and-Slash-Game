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

    void OnDisable()
    {
        TotalEnemies = 0;
        KillCount = 0;
    }
}
