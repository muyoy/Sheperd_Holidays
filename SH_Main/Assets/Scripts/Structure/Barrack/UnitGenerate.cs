﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct GenUnitInfo{
    public DBStruct.SheepData sheepData;
    public Sprite sheepImage;

    public GenUnitInfo(DBStruct.SheepData sheep, Sprite sprite)
    {
        sheepData = sheep;
        sheepImage = sprite;
    }
}

public class UnitGenerate : MonoBehaviour
{
    public enum UnitType { Sword = 0, Bow, Javelin }
    private Barrack barrackFunc;
    private GameManager GM;
    public UnitType unitType;

    private void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void TargetBarrack(GameObject barrack) // 배럭이 생성되거나 파괴되고 난 후에 다시 배럭을 지정해줘야 할 때 사용
    {
        barrackFunc = barrack.GetComponent<Barrack>();
    }

    public void AddUnit()
    {
        Sprite tempSprite = null;
        for (int i = 0; i < barrackFunc.UnitQueue.Length; i++)
        {
            if (barrackFunc.UnitQueue[i].activeInHierarchy)
            {
                continue;
            }
            else
            {
                switch (unitType)
                {
                    case UnitType.Sword:
                        if(GM.money < DBManager.instance.SheepDatas[1].cost) { break; }
                        tempSprite = Resources.Load("Unit/BarrackUnit/Sword_Icon", typeof(Sprite)) as Sprite;
                        GM.money -= DBManager.instance.SheepDatas[1].cost;
                        barrackFunc.GenUnitQueue.Enqueue(new GenUnitInfo(DBManager.instance.SheepDatas[1], tempSprite));                        
                        break;
                    case UnitType.Bow:
                        if (GM.money < DBManager.instance.SheepDatas[3].cost) { break; }
                        tempSprite = Resources.Load("Unit/BarrackUnit/Bow_Icon", typeof(Sprite)) as Sprite;
                        GM.money -= DBManager.instance.SheepDatas[3].cost;
                        barrackFunc.GenUnitQueue.Enqueue(new GenUnitInfo(DBManager.instance.SheepDatas[3], tempSprite));
                        break;
                    case UnitType.Javelin:
                        if (GM.money < DBManager.instance.SheepDatas[2].cost) { break; }
                        tempSprite = Resources.Load("Unit/BarrackUnit/Spear_Icon", typeof(Sprite)) as Sprite;
                        GM.money -= DBManager.instance.SheepDatas[2].cost;
                        barrackFunc.GenUnitQueue.Enqueue(new GenUnitInfo(DBManager.instance.SheepDatas[2], tempSprite));
                        break;
                }

                if(barrackFunc.UnitProductionCoroutine == null)
                {
                    barrackFunc.UnitProductionCoroutine = barrackFunc.StartCoroutine(barrackFunc.UnitProduction());
                }
                barrackFunc.UnitQueue[i].SetActive(true);
                barrackFunc.UnitQueue[i].GetComponent<SpriteRenderer>().sprite = tempSprite;

                break;
            }
        }
    }

}

