using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifesteal : WeaponUpgrade
{
	public override void ApplyEffects(WeaponBase weaponBase)
	{
		weaponBase.GetComponent<Sword>().SetLifestealAmount(upgradeData.uniqueNumber * 0.01f);
		weaponBase.GetComponent<Sword>().SetHasLifesteal();
	}
}
