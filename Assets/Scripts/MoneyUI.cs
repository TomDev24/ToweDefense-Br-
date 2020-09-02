using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    private Text moneyValue;

    private void Start()
    {
        moneyValue = GetComponent<Text>();    
    }

    void Update()
    {
        moneyValue.text = "$" + (PlayerStats.Money).ToString(); // do we need call it every frame?
    }
} 
