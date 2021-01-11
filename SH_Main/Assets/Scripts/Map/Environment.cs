﻿// ==============================================================
// 시간에 따른 맵 환경 관리
// ( 하늘, 땅, 산, 건물 등)
//
// AUTHOR: Yang SeEun
// CREATED: 2020-12-29
// UPDATED: 2021-01-05
// ==============================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private BattleManager battleManager;
    private SkyRotation sky;
    private OrbitMovement sunMoon;


    private SpriteRenderer mountian_renderer;
    private Sprite dayMountainImage;
    private Sprite nightMountainImage;

    public float duration = 3.0f;

    private void Awake()
    {
        if(battleManager == null) battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        sky = Camera.main.transform.Find("Sky").GetComponent<SkyRotation>();
        sunMoon = Camera.main.transform.Find("SunMoon").GetComponent<OrbitMovement>();

        mountian_renderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        dayMountainImage = Resources.Load<Sprite>("Sprite/Environment/Mountian/Morning_background_mountain");
        nightMountainImage = Resources.Load<Sprite>("Sprite/Environment/Mountian/night_background_mountain");
    }

    #region 임시Test
    private void Start()
    {
        //Chenge(true);
    }
    #endregion


    public void Chenge(bool isDay)
    {
        if(isDay)
        {
            StartCoroutine(ChangeToDaytime());
        }
        else
        {
            StartCoroutine(ChangeToNighttime());
        }
    }

    private IEnumerator ChangeToDaytime()
    {
        //하늘 회전
        StartCoroutine(sky.Rotation(-180.0f, duration));

        yield return new WaitForSeconds(duration*0.5f);

        //산 변경
        mountian_renderer.sprite = dayMountainImage;
        //TODO: 타일 변경
        //TODO: 건물 변경


        //해와 달 회전
        StartCoroutine(sunMoon.MoveEllipse(-90.0f,90.0f, battleManager.Daytime));
    }

    private IEnumerator ChangeToNighttime()
    {
        //하늘 회전
        StartCoroutine(sky.Rotation(0.0f, duration));
        //해와 달 회전
        StartCoroutine(sunMoon.MoveEllipse(-90.0f,90.0f, duration));

        yield return new WaitForSeconds(duration*0.5f);

        //산 변경
        mountian_renderer.sprite = nightMountainImage;
        //TODO: 타일 변경
        //TODO: 건물 변경
    }

}