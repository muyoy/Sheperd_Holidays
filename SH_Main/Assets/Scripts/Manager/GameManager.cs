using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int _money;
    public int money
    {
        get { return _money; }
        set
        {
            infoTopBar.ChangeMoneyLabel(_money, value);
            _money = value;
        }
    }

    private int _population;
    public int population
    {
        get { return _population; }
        set
        {
            infoTopBar.ChangeSheepLabel(_money, value);
            _population = value;
        }
    }
    public int maxPopulation;

    public InfoTopBar infoTopBar;

    private void Awake()
    {
        infoTopBar = GameObject.FindGameObjectWithTag("UIManager").transform.Find("Canvas").transform.Find("TopInterface").GetComponent<InfoTopBar>();

        money = 50;
        maxPopulation = 5;

    }

    
}
