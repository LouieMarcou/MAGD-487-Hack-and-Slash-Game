using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;

    [SerializeField] List<UpgradeButton> upgradeButtons;

    [SerializeField] UpgradeManager upgradeManager;

    [SerializeField] private GameObject player;

    [SerializeField] private EnemyManager enemyManager;

    void Start()
    {
        HideButtons();
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Clean();
        panel.SetActive(true);
        Time.timeScale = 0;
        player.GetComponent<MouseLook>().enabled = false;

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
		if(upgradeDatas[i] != null)
			{
            upgradeButtons[i].gameObject.SetActive(true);
            //Debug.Log(upgradeButtons[i].gameObject, upgradeButtons[i].gameObject);
            upgradeButtons[i].Set(upgradeDatas[i]);
			}
        }
    }

    public void Clean()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].Clean();
        }
    }

    public void Upgrade(int pressedButtonID)
    {
        //Debug.Log("Player pressed : " + pressedButtonID.ToString());
        upgradeManager.Upgrade(pressedButtonID);
        upgradeManager.AddToAvalibleUpgrades(pressedButtonID);
        ClosePanel();
    }

    public void ClosePanel()
    {
        //Debug.Log("closing panel");
        HideButtons();

        panel.SetActive(false);

        Time.timeScale = 1;
        player.GetComponent<MouseLook>().enabled = true;
        enemyManager.ResetTimer();
    }

    private void HideButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }
}
