using UnityEngine;

public class PlayerFlatStatIncrease : PlayerUpgradeBase
{
    public override void ApplyEffects(PlayerController player)
	{
		player.playerData.stats.health += 25f;
		player.AddHealth(25f);
		player.playerData.stats.stamina += 25f;
		//player.playerData.stats.health += 1f;
		
	}
}
