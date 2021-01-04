//************************************************************
//
//  EDITOR : KIM JIHUN
//  LAST UPDATE : 2020.12.24
//  Script Purpose :  Fundamental Script of All Structures
//
//************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    protected virtual void BuildingDestroy(){ /* 건물 파괴 기능 생길 시 */ } 
    public virtual int HpChange(float damage){ return HP; }

    public virtual void Init()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = structureBody;
    }
   
}
