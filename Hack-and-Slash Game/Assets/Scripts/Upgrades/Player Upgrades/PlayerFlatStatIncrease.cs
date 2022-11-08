using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlatStatIncrease : MonoBehaviour
{
	public UpgradeData upgradeData;
	
    public void ApplyEffects(PlayerController player)
	{
		player.playerData.stats.health += 50f;
		player.playerData.stats.stamina += 50f;
		player.playerData.stats.health += 1f;
		
	}
}
