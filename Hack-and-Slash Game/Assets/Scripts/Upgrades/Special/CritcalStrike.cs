using UnityEngine;

public class CritcalStrike : PlayerUpgradeBase
{
    public override void ApplyEffects(PlayerController player)
    {
        player.ActivateCrit(2f, upgradeData.uniqueNumber);
    }
}
