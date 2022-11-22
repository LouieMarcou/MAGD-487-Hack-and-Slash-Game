using UnityEngine;

public abstract class PlayerUpgradeBase : MonoBehaviour
{
    public UpgradeData upgradeData;

    public abstract void ApplyEffects(PlayerController player);
}
