using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedIncrease : PlayerUpgradeBase
{
    public override void ApplyEffects(PlayerController player)
    {
        player.playerData.stats.speed += upgradeData.uniqueNumber;
    }
}
