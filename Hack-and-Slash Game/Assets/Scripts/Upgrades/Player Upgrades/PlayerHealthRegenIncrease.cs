using UnityEngine;

public class PlayerHealthRegenIncrease : PlayerUpgradeBase
{
    public override void ApplyEffects(PlayerController player)
    {
        player.IncreaseHealthRegen(upgradeData.uniqueNumber);
    }
}
