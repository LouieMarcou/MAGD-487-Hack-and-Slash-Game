using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum UpgradeType
{
    Player,
    Weapon,
    Special
}

[CreateAssetMenu]
public class UpgradeData : ScriptableObject
{
    public string Name;
    public UpgradeType upgradeType;
    public GameObject UpgradeBasePrefab;
    public float uniqueNumber;
    public bool unique;
    public Sprite icon;
    public string UpgradeText;
}
