﻿//************************************
//
//  EDITOR : KIM JIHUN
//  LAST UPDATE : 2020.11.17
//  Script Purpose :  UI Manager
//
//***********************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct GroundInfo
{
    public GameObject ground;
    public bool IsEmpty;

    public GroundInfo(GameObject gameObjectTile, bool tf)
    {
        ground = gameObjectTile;
        IsEmpty = tf;
    }
};

public class UIManager : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject MorningMap; // 아침 맵
    public GameObject EveningMap; // 저녁 맵
    private List<GroundInfo> morningGround;
    private List<GroundInfo> eveningGround;

    private void Awake()
    {
        morningGround = new List<GroundInfo>();
        eveningGround = new List<GroundInfo>();
=======
    public GameObject GroundMap;
    public GroundInfo[] Ground = new GroundInfo[45];
    public Sprite DayGround;
    public Sprite NightGround;

    private UIManager uiManager;

    private void Awake()
    {
        if(uiManager == null)
        {
            uiManager = this;
        }
        else
        {
            Destroy(uiManager);
            uiManager = this;
        }
>>>>>>> StructureDrag
    }

    public GroundInfo[] MapGetter() // Map 게임오브젝트에 들어있는 Image를 리스트에 삽입
    {
<<<<<<< HEAD
        for (int i = 0; i < MorningMap.transform.childCount; i++)
        {
            morningGround.Add(new GroundInfo(MorningMap.transform.GetChild(i).GetComponent<Image>(), true));
        }
        for (int i = 0; i < EveningMap.transform.childCount; i++)
        {
            eveningGround.Add(new GroundInfo(EveningMap.transform.GetChild(i).GetComponent<Image>(), true));
=======
        for (int i = 0; i < GroundMap.transform.childCount; i++)
        {
            Ground[i] = new GroundInfo(GroundMap.transform.GetChild(i).gameObject, true);
>>>>>>> StructureDrag
        }

        return Ground;
    }

    public void GroundSet(bool IsDay)
    {
        if (IsDay)
        {
<<<<<<< HEAD
            for (int i = 0; i < morningGround.Count; i++)// 아침이면 아침 땅 활성화
            {
                morningGround[i].ground.gameObject.SetActive(true);
            }
            for (int i = 0; i < eveningGround.Count; i++)
            {
                eveningGround[i].ground.gameObject.SetActive(false);
=======
            for (int i = 0; i < Ground.Length; i++)// 아침이면 아침 땅 활성화
            {
                Ground[i].ground.GetComponent<SpriteRenderer>().sprite = DayGround;
>>>>>>> StructureDrag
            }
        }
        else
        {
<<<<<<< HEAD
            for (int i = 0; i < morningGround.Count; i++)// 밤이면 아침 땅 비활성화
            {
                morningGround[i].ground.gameObject.SetActive(false);
            }
            for (int i = 0; i < eveningGround.Count; i++)
            {
                eveningGround[i].ground.gameObject.SetActive(true);
=======
            for (int i = 0; i < Ground.Length; i++)// 밤이면 아침 땅 비활성화
            {
                Ground[i].ground.GetComponent<SpriteRenderer>().sprite = NightGround;
>>>>>>> StructureDrag
            }
            
        }
    }
}