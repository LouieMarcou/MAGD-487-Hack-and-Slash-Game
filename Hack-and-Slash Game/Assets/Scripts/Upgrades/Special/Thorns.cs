using UnityEngine;

public class Thorns : PlayerUpgradeBase
{
    public override void ApplyEffects(PlayerController player)
    {
        player.ActivateThorns(upgradeData.uniqueNumber);
    }
}
