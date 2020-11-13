﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{

    public Sprite structureBody;
    protected int hitPoints;
    public int HP{
        get { return hitPoints; }
        set {
            hitPoints = value;
        }
    }

    [SerializeField]protected int cost;
    [SerializeField]protected float buildTime; // 건물이 지어질 때 필요한 시간
    public int buildingSpace; // 건물이 지어질 때 필요한 공간

    protected virtual void BuildingFunc(){ /* TODO : 건물 각각의 기능 */ }
    protected virtual void BuildingConstruct(){}
    protected virtual void BuildingDestroy(){}
    protected virtual int HpChange(float damage){ return HP; }

    public virtual void Init()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = structureBody;
    }
   
}
