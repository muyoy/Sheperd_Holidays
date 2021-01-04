//************************************
//
//  EDITOR : KIM JIHUN
//  LAST UPDATE : 2020.11.17
//  Script Purpose :  GameManager
//
//***********************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public BattleManager BM;
    public int money;
    public int population;
    public int maxPopulation;

    private void Start()
    {
        money = 50;
        maxPopulation = 5;
    }
}
