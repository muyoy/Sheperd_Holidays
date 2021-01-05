//************************************
//
//  EDITOR : KIM JIHUN
//  LAST UPDATE : 2020.11.17
//  Script Purpose :  GameManager
//
//***********************************

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public BattleManager BM;
    public TextMeshProUGUI sheepnum;
    public TextMeshProUGUI money_text;
    public TextMeshProUGUI day;
    public int money;
    public int population;
    public int maxPopulation;

    private void Start()
    {
        money = 50;
        maxPopulation = 5;

        day.text = "DAY : "+ BM.currentWave.ToString();
        money_text.text = money.ToString();
        sheepnum.text = population.ToString();
    }
}
