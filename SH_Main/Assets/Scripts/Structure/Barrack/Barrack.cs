//************************************************************
//
//  EDITOR : KIM JIHUN
//  LAST UPDATE : 2021.1.6
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
    public GameObject[] UnitQueue;
    private UIManager UI;
    public GameObject[] GenUnitImg;
    public Queue<GenUnitInfo> GenUnitQueue = new Queue<GenUnitInfo>();
    private float timer;
    private bool IsGenerating = false; // 지금 유닛을 생산하고 있는가?
    private bool isActive = true; // 지금 Barrack을 클릭한 상태인가?

    #region Coroutines
    public Coroutine UnitProductionCoroutine = null;
    #endregion Coroutines


    private void Awake()
    {
        UIManager = GameObject.Find("UIManager");
        UI = UIManager.GetComponent<UIManager>();

        GenUnitImg = UI.UnitRepIcon;
        InitUnitQueue();
    }

    private void Start()
    {
        Init();
        for (int i = 0; i < GenUnitImg.Length; i++)
        {
            GenUnitImg[i].GetComponent<UnitGenerate>().TargetBarrack(gameObject);
        }

    }

    private void InitUnitQueue()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            UnitQueue[i] = transform.GetChild(i).gameObject;
            UnitQueue[i].SetActive(false);
        }
    }


    public IEnumerator UnitProduction() // 유닛 생산시간 관리
    {
        DBStruct.SheepData GeneratingUnit;
        while (GenUnitQueue.Count > 0)
        {
#if UNITY_EDITOR
            Debug.Log("GenUnitQueue : " + GenUnitQueue.Count);
            Debug.Log("UnitProduction : " + UnitProductionCoroutine);
#endif
            GeneratingUnit = GenUnitQueue.Peek().sheepData;
            IsGenerating = true;
            StartCoroutine(Clock());

            // Clock 스타트?

            while (timer < GeneratingUnit.waitingTime) {
#if UNITY_EDITOR
                Debug.Log("timer : " + timer);
                Debug.Log("Target Time : " + GeneratingUnit.waitingTime);
#endif 
                yield return null; 
            }
            if (timer >= GeneratingUnit.waitingTime)
            {
#if UNITY_EDITOR
                Debug.Log("if 문 실행");
#endif 
                StopCoroutine(Clock());
                InstantiateUnit(GeneratingUnit);
                for (int i = 0; i < GenUnitQueue.Count; i++)
                {
                    if (i < GenUnitQueue.Count - 1 && i>=0)
                    {
                        UnitQueue[i].GetComponent<SpriteRenderer>().sprite = UnitQueue[i + 1].GetComponent<SpriteRenderer>().sprite;
                    }
                    else
                    {
                        UnitQueue[i].SetActive(false);
                    }
                }
            }

            if(GenUnitQueue.Count <= 0)
            {
                UnitProductionCoroutine = null;
                StopCoroutine(UnitProduction());
            }
            yield return null;
        }
        yield return null;
    }

    private IEnumerator Clock()
    {
        timer = 0;
        while (IsGenerating)
        {
            timer++;
            yield return new WaitForSeconds(1);
        }
        yield return null;
    }

    private void InstantiateUnit(DBStruct.SheepData sheep)
    {

    }
}
