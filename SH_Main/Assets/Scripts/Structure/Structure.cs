//************************************************************
//
//  EDITOR : KIM JIHUN
//  LAST UPDATE : 2020.11.17
//  Script Purpose :  Fundamental Script of All Structures
//
//************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Structure : MonoBehaviour
{
    protected BattleManager BM;
    protected virtual void Start()
    {
        BM = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    protected int hitPoints;
    public int HP{
        get { return hitPoints; }
        set {
            hitPoints = value;
            if(hitPoints <= 0)
            {
                BuildingDisrupt();
            }
        }
    }

    [SerializeField]protected int cost;
    [SerializeField]protected float buildTime; // 건물이 지어질 때 필요한 시간
    public int buildingSpace; // 건물이 지어질 때 필요한 공간
    public Sprite dayImage; // 낮에 나타나는 건물
    public Sprite nightImage; // 밤에 나타나는 건물
    public Sprite disruptImage; // 파괴 되었을때 나타나는 건물

    protected virtual void BuildingFunc(){ /* TODO : 건물 각각의 기능 */ }
    protected virtual void BuildingDestroy(){ /* 건물 파괴 기능 생길 시 */ } 
    public virtual int HpChange(float damage){ return HP; }

    public virtual void ChangeStructImage()
    {
        if (BM.isDay)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = dayImage;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = nightImage;
        }
    }

    protected virtual void BuildingDisrupt()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = disruptImage;
    }

    public virtual void Init()
    {
        ChangeStructImage();
    }
}