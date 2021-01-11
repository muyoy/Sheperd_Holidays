using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Structure
{
    private GameManager GM;

    protected override void Start()
    {
        base.Start();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        GM.maxPopulation += 10;
    }


}
