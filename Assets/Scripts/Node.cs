using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [SerializeField]
    private Color hoverColor;
    [SerializeField]
    private Color notEnoughMoneyColor;

    public Vector3 offset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color defaultColor;

    private BuildManager buildManager;


    private void Start()
    {
        rend = GetComponent<Renderer>();
        defaultColor = rend.material.GetColor("_BaseColor");
        buildManager = BuildManager.instance;
    }
    
    public Vector3 GetBuildPosition()
    {
        return transform.position + offset;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) 
            return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        //buildManager.BuildTurretOn(this); moved script build turretOn, inside here
        BuildTurret(buildManager.turretToBuild);
    }

    private void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            return;
        }

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity) as GameObject;
        Destroy(effect, 5f);

        PlayerStats.Money -= blueprint.cost;
        turretBlueprint = blueprint;

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity) as GameObject;
        turret = _turret;
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            return;
        }

        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity) as GameObject;
        Destroy(effect, 5f);

        PlayerStats.Money -= turretBlueprint.upgradeCost;
        Destroy(turret);

        //building a new turret
        GameObject _turret = Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity) as GameObject;
        turret = _turret;

        isUpgraded = true;
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        Destroy(turret);

        turretBlueprint = null;

        //Spawn a Cool effect;
        GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity) as GameObject;
        Destroy(effect, 5f);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // if pointer over ui, do not interact with the game
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.hasMoney)
        {
            rend.material.SetColor("_BaseColor", hoverColor); // instead of rend.material.color //solution found with debug mode of unity
        } else
        {
            rend.material.SetColor("_BaseColor", notEnoughMoneyColor);
        }
    }

    private void OnMouseExit()
    {
        rend.material.SetColor("_BaseColor", defaultColor);
    }
}
