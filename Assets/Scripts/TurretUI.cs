using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour
{
    [SerializeField]
    private Text upgradeCost;
    [SerializeField]
    private Text sellTxt;

    [SerializeField]
    private Button upgradeBtn;

    private Node target;
    [SerializeField]
    private GameObject canvas;

    public void SetTarget(Node _target)
    {
        this.target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeBtn.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeBtn.interactable = false;
        }

        sellTxt.text = "$" + target.turretBlueprint.GetSellAmount();

        canvas.SetActive(true);
    }

    public void Hide()
    {
        canvas.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
