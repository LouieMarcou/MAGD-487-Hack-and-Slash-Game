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

    private GameObject upgradeGameObject;


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
		avalibleUpgrades.RemoveAt(selectedUpgradeId);

        AddUpgrade(upgradeData);

    }

    public void AddUpgrade(UpgradeData upgradeData)
    {
        upgradeGameObject = Instantiate(upgradeData.UpgradeBasePrefab, upgradeObjectsContainer);

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.Player:
                upgradeGameObject.GetComponent<PlayerFlatStatIncrease>().ApplyEffects(player);//fix
				player.SetData(player.playerData);//fix
                break;
            case UpgradeType.Weapon:
                upgradeGameObject.GetComponent<WeaponUpgrade>().ApplyEffects(player.GetWeapon());
                player.GetWeapon().SetData(player.GetWeapon().weaponData);
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
            //upgradeList.Add(avalibleUpgrades[Random.Range(0, avalibleUpgrades.Count)]);
            upgradeList.Add(avalibleUpgrades[i]);//add rarity???
        }


        return upgradeList;
    }
	
}
