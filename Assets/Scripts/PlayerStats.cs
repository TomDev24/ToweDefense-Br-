using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int startMoney = 400;
    [SerializeField]
    private int startLives = 20;

    public static int Rounds;

    public static int Money; // static variable will be carry on from one scene to another
    public static int Lives;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
        Rounds = 0;
    }
    
}
