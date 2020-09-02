using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject buildEffect;
    [SerializeField]
    private TurretUI turretUI;

    //E11 is very important, about currency, refactoring and making cleaner code
    //  public GameObject standartTurretPrefab;
    //  public GameObject missileLauncherPrefab;

    public TurretBlueprint turretToBuild { get; private set; } // Before it was a gameObject
    private Node selectedNode;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool hasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public static BuildManager instance;
    public GameObject sellEffect;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is more than one buildManager instance");
            return;
        }
        instance = this;
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;

        DeselectNode();
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        turretUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        turretUI.Hide();
    }

}
