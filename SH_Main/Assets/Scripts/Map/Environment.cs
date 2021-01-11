// ==============================================================
// 시간에 따른 맵 환경 관리
// ( 하늘, 땅, 산, 건물 등)
//
// AUTHOR: Yang SeEun
// CREATED: 2020-12-29
// UPDATED: 2021-01-12
// ==============================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    private BattleManager battleManager;

    private SkyRotation sky;
    public PlanetController planet;
    public float duration = 3.0f;

    private SpriteRenderer mountian_renderer;
    private Sprite dayMountainImage;
    private Sprite nightMountainImage;

    private GameObject tileParentObj = null;

    private void Awake()
    {
        if (battleManager == null) battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        sky = Camera.main.transform.Find("Sky").GetComponent<SkyRotation>();
        planet = Camera.main.transform.Find("Planet").GetComponent<PlanetController>();

        mountian_renderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        dayMountainImage = Resources.Load<Sprite>("Sprite/Environment/Mountian/Morning_background_mountain");
        nightMountainImage = Resources.Load<Sprite>("Sprite/Environment/Mountian/night_background_mountain");

        tileParentObj = GameObject.Find("TileMap").transform.Find("Tile").gameObject;
    }

    private void Start()
    {
        //해 회전
        StartCoroutine(planet.MoveEllipse(-90.0f, 90.0f, battleManager.Daytime));
    }


    public void Chenge(bool isDay)
    {
        if (isDay)
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
        //행성 
        StartCoroutine(PlanetController_ToDay());
        yield return new WaitForSeconds(duration * 0.5f);

        //산 변경
        mountian_renderer.sprite = dayMountainImage;
        //건물 변경
        ChnageBuilding();


    }


    private IEnumerator ChangeToNighttime()
    {
        //하늘 회전
        StartCoroutine(sky.Rotation(0.0f, duration));
        //행성 
        StartCoroutine(PlanetController_ToNignt());
        yield return new WaitForSeconds(duration * 0.5f);

        //산 변경
        mountian_renderer.sprite = nightMountainImage;
        //TODO: 건물 변경
        ChnageBuilding();
    }

    #region 행성 관리
    /// <summary>
    /// 아침으로 바뀌었을 때 행성 관리
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlanetController_ToDay()
    {
        //이미지 변경
        planet.ChangedInTime(false, battleManager.currentWave);
        //달 회전
        yield return StartCoroutine(planet.MoveEllipse(-45.0f, 90.0f, duration));

        //이미지 변경
        planet.ChangedInTime(true, battleManager.currentWave);
        //해 회전
        StartCoroutine(planet.MoveEllipse(-90.0f, 90.0f, battleManager.Daytime));
    }
    /// <summary>
    /// 밤으로 바뀌었을 때 행성 관리
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlanetController_ToNignt()
    {
        //이미지 변경
        planet.ChangedInTime(false, battleManager.currentWave);
        //달 회전
        StartCoroutine(planet.MoveEllipse(-90.0f, -45.0f, 2.0f));
        yield return null;
    }
    #endregion

    private void ChnageBuilding()
    {
        Structure[] buildings = LoadStructure();
        for (int i=0; i< buildings.Length;i++)
        {
            //TODO : 주석 풀기
            //buildings[i].ChangeStructImage();
            Debug.Log(buildings[i].name);
        }
    }

    private Structure[] LoadStructure()
    {
        return tileParentObj.transform.GetComponentsInChildren<Structure>();
    }
}
