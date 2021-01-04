// ==============================================================
// 시간에 따른 맵 환경 관리
// ( 하늘, 땅, 산, 건물 등)
//
// AUTHOR: Yang SeEun
// CREATED: 2020-12-29
// UPDATED: 2021-01-04
// ==============================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private BattleManager battleManager;
    private SkyRotation sky;


    private SpriteRenderer bg_spriteRenderer;
    private Sprite dayMountainImage;
    private Sprite nightMountainImage;

    public float duration = 3.0f;

    private void Awake()
    {
        if(battleManager != null) battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        sky = GameObject.FindGameObjectWithTag("MainCamera").transform.Find("Sky").GetComponent<SkyRotation>();
        bg_spriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();

        dayMountainImage = Resources.Load<Sprite>("Sprite/Environment/Mountian/Morning_background_mountain");
        nightMountainImage = Resources.Load<Sprite>("Sprite/Environment/Mountian/night_background_mountain");
    }

    #region Test
    private void Start()
    {
        Chenge(true);
    }
    #endregion


    private void Chenge(bool isDay)
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
        StartCoroutine(sky.Rotation(179.0f, duration));
        //TODO: 해와 달 회전

        yield return new WaitForSeconds(duration*0.5f);

        //산 변경
        bg_spriteRenderer.sprite = dayMountainImage;
        //TODO: 타일 변경
        //TODO: 건물 변경
    }
    private IEnumerator ChangeToNighttime()
    {
        StartCoroutine(sky.Rotation(0f, duration));

        yield return new WaitForSeconds(duration*0.5f);
        bg_spriteRenderer.sprite = nightMountainImage;

    }
}
