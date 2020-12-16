//************************************************************
//
//  EDITOR : KIM JIHUN
//  LAST UPDATE : 2020.12.03
//  Script Purpose :  Fundamental Script of All Barracks
//
//************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public struct UnitInfo  // 병영에서 생산되는 유닛의 기본 정보
{
    Image Unit;  // 병영에 표시되는 유닛의 이미지
    float CoolTime;  // 유닛이 생산하고 다음 생산이 가능하기까지 걸리는 시간

    public UnitInfo(Image unit, float coolTime)
    {
        Unit = unit;
        CoolTime = coolTime;
    }
}

public class Barrack : Structure
{
    public List<GameObject> UnitPrefab = new List<GameObject>(); // 인스턴스화 시킬 유닛
    public List<Sprite> unitImage = new List<Sprite>(); // UI에 보여줄 샘플 유닛 이미지 ( 그래픽에게 따로 요청 )
    public GameObject[] unitSpriteQueue; 
    [SerializeField] private const int MAXIMUM_UNIT_QUEUE = 1; // 한 건물당 생산할 수 있는 최대 유닛의 갯수 3마리
    protected bool IsReady = true; // 병력 생산 가능 판별
    public float coolTime = 7;

    protected float timer;

    protected int bootNum; // 신병들을 구분하는 ID

    private void Start()
    {
        unitSpriteQueue = GameObject.FindGameObjectsWithTag("UnitQueue");
    }

    /// <summary>
    /// UnitPrefab 에 담겨있는 unitImage 를 건물을 클릭할때마다 교체
    /// unitImage에 Instantiate 하는 Sprite는 UnitPrefab.name으로 검색
    /// </summary>
    public override void Init()
    {
        base.Init();
    }

    private void OnClickEvent()
    {
#if UNITY_EDITOR
        Debug.Log("click barrack");
#endif
        ClickedBarrack();
    }

    private void ClickedBarrack()
    {
        for (int i = 0; i < unitSpriteQueue.Length; i++)
        {
            Debug.Log(unitSpriteQueue[i]);
            unitSpriteQueue[i].GetComponent<Image>().sprite = unitImage[i];
        }
    }

    protected IEnumerator UnitTimer()
    {
        timer = 0;
        while (!IsReady)
        {
            timer += Time.deltaTime;
            if (timer >= 7)
            {
                IsReady = true;
            }
            yield return null;
        }
        yield return null;
    }

    
}
