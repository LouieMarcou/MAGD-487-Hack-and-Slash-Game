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
        avalibleUpgrades.Add(player.playerData.regularUpgrades[Random.Range(0, player.playerData.regularUpgrades.Count)]);
        avalibleUpgrades.Add(player.playerData.specialUpgrades[Random.Range(0, player.playerData.specialUpgrades.Count)]);
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Upgrade(int selectedUpgradeId)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeId];
        avalibleUpgrades[selectedUpgradeId] = null;

        AddUpgrade(upgradeData);

    }

    public void AddUpgrade(UpgradeData upgradeData)
    {
        upgradeGameObject = Instantiate(upgradeData.UpgradeBasePrefab, upgradeObjectsContainer);
        //Debug.Log(upgradeData);

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.Player:
                upgradeGameObject.GetComponent<PlayerUpgradeBase>().ApplyEffects(player);
				player.SetData(player.playerData);
                break;
            case UpgradeType.Special:
                upgradeGameObject.GetComponent<PlayerUpgradeBase>().ApplyEffects(player);
                player.GetComponent<PlayerController>().playerData.specialUpgrades.Remove(upgradeData);
                break;
            case UpgradeType.Weapon:
                upgradeGameObject.GetComponent<WeaponUpgrade>().ApplyEffects(player.GetWeapon());
                player.GetWeapon().SetData(player.GetWeapon().weaponData);
                if (upgradeData.unique)
                {
                    player.GetWeapon().weaponData.upgrades.Remove(upgradeData);
                }
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
        return avalibleUpgrades;
        //List<UpgradeData> upgradeList = new List<UpgradeData>();
        //if (count > avalibleUpgrades.Count)
        //{
        //    count = avalibleUpgrades.Count;
        //}
        //for (int i = 0; i < count; i++)
        //{
        //    if(i == 0)
        //    {
        //        upgradeList.Add(player.playerData.regularUpgrades[Random.Range(0, player.playerData.regularUpgrades.Count)]);
        //        //upgradeList.Add(player.playerData.regularUpgrades[Random.Range(0, player.playerData.regularUpgrades.Count)]);
        //    }
        //    else if(i == 1)
        //    {
        //        upgradeList.Add(player.playerData.specialUpgrades[Random.Range(0, player.playerData.specialUpgrades.Count)]);

        //    }
        //    if(i == 2)
        //    {
        //        upgradeList.Add(player.GetWeapon().weaponData.upgrades[Random.Range(0, player.GetWeapon().weaponData.upgrades.Count)]);
        //    }
        //    //upgradeList.Add(avalibleUpgrades[Random.Range(0, avalibleUpgrades.Count)]);
        //    //upgradeList.Add(avalibleUpgrades[i]);//add rarity???
        //}


        //return upgradeList;
    }

    public void AddToAvalibleUpgrades(int index)
    {
        if (index == 0)
        {
            if(player.playerData.regularUpgrades.Count == 0)
            {
                return;
            }
            //upgrade = player.playerData.regularUpgrades[Random.Range(0, player.playerData.regularUpgrades.Count)];
            avalibleUpgrades[0] = player.playerData.regularUpgrades[Random.Range(0, player.playerData.regularUpgrades.Count)];
            //avalibleUpgrades.Add(player.playerData.regularUpgrades[Random.Range(0, player.playerData.regularUpgrades.Count)]);
        }
        else if (index == 1)
        {
            if (player.playerData.specialUpgrades.Count == 0)
            {
                return;
            }
            avalibleUpgrades[1] = player.playerData.specialUpgrades[Random.Range(0, player.playerData.specialUpgrades.Count)];
            //upgrade = player.playerData.specialUpgrades[Random.Range(0, player.playerData.specialUpgrades.Count)];
            //avalibleUpgrades.Add(upgrade);
        }
        if (index == 2)
        {
            if (player.GetWeapon().weaponData.upgrades.Count == 0)
            {
                return;
            }
            avalibleUpgrades[2] = player.GetWeapon().weaponData.upgrades[Random.Range(0, player.GetWeapon().weaponData.upgrades.Count)];
            //upgrade = player.GetWeapon().weaponData.upgrades[Random.Range(0, player.GetWeapon().weaponData.upgrades.Count)];
            //fix remove thing
        }
    }

	
    public List<UpgradeData> GetUpgradeDatas()
    {
        return avalibleUpgrades;
    }
}
