using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]  // so we can edit value in editor
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;

    public int upgradeCost; //internal doesnt show in the inspector
    public GameObject upgradedPrefab;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
