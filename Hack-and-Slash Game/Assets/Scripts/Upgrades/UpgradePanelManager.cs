using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject panel;

    [SerializeField] List<UpgradeButton> upgradeButtons;

    [SerializeField] private GameObject player;

    void Start()
    {
        HideButtons();
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Clean();
        panel.SetActive(true);
        Time.timeScale = 0;

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            //Debug.Log(upgradeButtons[i].gameObject, upgradeButtons[i].gameObject);
            upgradeButtons[i].Set(upgradeDatas[i]);
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
        //player.GetComponent<LevelUp>().Upgrade(pressedButtonID);
        ClosePanel();
    }

    public void ClosePanel()
    {
        HideButtons();

        panel.SetActive(false);
        Time.timeScale = 1;
    }

    private void HideButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }
}
