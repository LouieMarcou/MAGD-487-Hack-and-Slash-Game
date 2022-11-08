using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text description;

    public void Set(UpgradeData upgradeData)
    {
        icon.sprite = upgradeData.icon;
        description.text = upgradeData.UpgradeText;
    }

    internal void Clean()
    {
        icon.sprite = null;
    }
}
