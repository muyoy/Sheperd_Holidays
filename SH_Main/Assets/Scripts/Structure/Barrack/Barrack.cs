//************************************************************
//
//  EDITOR : KIM JIHUN
//  LAST UPDATE : 2020.12.24
//  Script Purpose :  Fundamental Script of All Barracks
//  하단 인터페이스에서 유닛 생성 버튼을 누르면 유닛 생산 대기열을 나타나게 하고 시간을 표시, 시간이 다 되면 유닛 생성
//
//************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct UnitInfo  // 병영에서 생산되는 유닛의 기본 정보
{
    public Sprite UnitSprite;  // 병영에 표시되는 유닛의 이미지
    public float CoolTime;  // 유닛이 생산하고 다음 생산이 가능하기까지 걸리는 시간

    public UnitInfo(Sprite unitSprite, float coolTime)
    {
        UnitSprite = unitSprite;
        CoolTime = coolTime;
    }
}

public class Barrack : Structure
{
    public override void Init()
    {
        base.Init();
    }

    public List<UnitInfo> UnitList = new List<UnitInfo>();  // 생산 가능한 유닛들의 리스트
    public GameObject UIManager;
    private UIManager UI;
    
    private bool isActive = true; // 지금 Barrack을 클릭한 상태인가?

    private void Awake()
    {
        UIManager = GameObject.Find("UIManager");
        UI = UIManager.GetComponent<UIManager>();
    }

    
}
