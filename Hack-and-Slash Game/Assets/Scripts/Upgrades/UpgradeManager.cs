using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [SerializeField] private UpgradePanelManager upgradePanel;

    [SerializeField] Transform upgradeObjectsContainer;

    [SerializeField] List<UpgradeData> avalibleUpgrades;
    List<UpgradeData> selectedUpgrades;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade(int selectedUpgradeId)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeId];



    }

    public void AddUpgrade(UpgradeData upgradeData)
    {
        GameObject upgradeGameObject = Instantiate(upgradeData.UpgradeBasePrefab, upgradeObjectsContainer);

        switch(upgradeData.upgradeType)
        {
            case UpgradeType.Player:

                break;
            case UpgradeType.Weapon:
                upgradeGameObject.GetComponent<WeaponBuff>().ApplyEffects(player.GetWeapon());
                break;
            case UpgradeType.Special:

                break;
        }

        player.GetComponent<PlayerController>().AddUpgrade(upgradeData);
    }

    public void Run()
    {
        if (selectedUpgrades == null)
        {
            selectedUpgrades = new List<UpgradeData>();
        }
        selectedUpgrades.Clear();
        selectedUpgrades.AddRange(GetUpgrades(3));

        upgradePanel.OpenPanel(GetUpgrades(3));
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();
        if (count > avalibleUpgrades.Count)
        {
            count = avalibleUpgrades.Count;
        }
        for (int i = 0; i < count; i++)
        {
            //upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
            upgradeList.Add(avalibleUpgrades[i]);
        }


        return upgradeList;
    }
}
