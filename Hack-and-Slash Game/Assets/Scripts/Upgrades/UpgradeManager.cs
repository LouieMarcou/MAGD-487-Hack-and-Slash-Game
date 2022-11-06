using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [SerializeField] private GameObject upgradePanel;

    [SerializeField] Transform upgradeObjectsContainer;

    [SerializeField] List<UpgradeData> avalibleUpgrades;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddUpgrade(UpgradeData upgradeData)
    {
        GameObject upgradeGameObject = Instantiate(upgradeData.UpgradeBasePrefab, upgradeObjectsContainer);

        switch(upgradeData.upgradeType)
        {
            case UpgradeType.Player:

                break;
            case UpgradeType.Weapon:

                break;
            case UpgradeType.Special:

                break;
        }

        player.GetComponent<PlayerController>().AddUpgrade(upgradeData);
    }
}
