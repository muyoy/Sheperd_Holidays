//************************************************
//
//  EDITOR : KIM JIHUN
//  LAST UPDATE : 2020.11.18
//  Script Purpose :  Control Ifantry barracks
//
//************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : Barrack
{
    public const int INFANTRY_COST = 150;
    public const int INFANTRY_HP = 1500;
    public const int INFANTRY_SPACE = 2;

    public GameObject boot; // 신병

    #region Coroutine
    Coroutine UnitTimerCoroutine;
    #endregion

    private void Awake()
    {
        buildingSpace = INFANTRY_SPACE;
    }

    public override void Init()
    {
        base.Init();

        cost = INFANTRY_COST;
        HP = INFANTRY_HP;

    }

    protected override void BuildingFunc()
    {
        // 신병 객체화
        boot = Instantiate(Resources.Load("Unit/" + UnitPrefab[bootNum].name, typeof(GameObject)) as GameObject,
            transform.position /* + offset */ , Quaternion.identity);
    }

    /// <summary>
    /// OnClick Method
    /// </summary>
    /// <param name="bootNum">한 건물에 존재하는 여러 종류의 유닛들을 구분하는 ID</param>
    public void DisciplineUnit(int bootNum)
    {
        this.bootNum = bootNum;
        IsReady = false;  // 병력 생산 불가능 상태
        if (UnitTimerCoroutine == null)
        {
            UnitTimerCoroutine = StartCoroutine(UnitTimer());
        }
        BuildingFunc();
    }
}
