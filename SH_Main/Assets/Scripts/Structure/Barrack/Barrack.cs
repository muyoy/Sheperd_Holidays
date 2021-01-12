//************************************************************
//
//  EDITOR : KIM JIHUN
//  LAST UPDATE : 2021.1.8
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

    public List<UnitInfo> UnitList = new List<UnitInfo>();  // 생산 가능한 유닛들의 리스트
    public GameObject[] UnitQueue;
    public UIManager UI;
    private GameManager GM;
    private BattleManager BM;
    public GameObject[] GenUnitImg;
    public Queue<GenUnitInfo> GenUnitQueue = new Queue<GenUnitInfo>();
    private GameObject timerObject;
    public float timer;
    private bool IsGenerating = false; // 지금 유닛을 생산하고 있는가?

    #region Coroutines
    public Coroutine UnitProductionCoroutine = null;
    private Coroutine ClockCoroutine = null;
    #endregion Coroutines

    public override void Init()
    {
        base.Init();
    }

    private void Awake()
    {
        UI = GameObject.Find("UIManager").GetComponent<UIManager>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        BM = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        timerObject = transform.Find("Canvas").Find("LeftTime").gameObject;
        
        GenUnitImg = UI.UnitRepIcon;
        InitUnitQueue();
    }

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < GenUnitImg.Length; i++)
        {
            GenUnitImg[i].GetComponent<UnitGenerate>().TargetBarrack(gameObject);
        }
        timerObject.SetActive(false);
    }


    private void LateUpdate()
    {
        timerObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(-0.85f,1.8f,0));
    }

    private void InitUnitQueue()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Contains("Queue"))
            {
                UnitQueue[i] = transform.GetChild(i).gameObject;
                UnitQueue[i].SetActive(false);
            }
        }
    }


    public IEnumerator UnitProduction() // 유닛 생산시간 관리
    {
        DBStruct.SheepData GeneratingUnit;

        while (GenUnitQueue.Count > 0)
        {
            timerObject.SetActive(true);
            GeneratingUnit = GenUnitQueue.Peek().sheepData;
            IsGenerating = true;
            if(ClockCoroutine == null)
            {
                ClockCoroutine = StartCoroutine(Clock());
            }
            

            while (timer < GeneratingUnit.waitingTime) { // 타이머 다 돌때까지 무한대기
                timerObject.GetComponent<Text>().text = (GeneratingUnit.waitingTime -timer).ToString();
                yield return null;
            }

            while (GM.population >= GM.maxPopulation)  // 인구수가 꽉차면 대기
            {
                StopCoroutine(ClockCoroutine);
                yield return null;
            }

            if (timer >= GeneratingUnit.waitingTime)
            {
                timerObject.SetActive(false);
                StopCoroutine(ClockCoroutine);
                ClockCoroutine = null;
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

                GM.population++; // 인구수 증가
                GenUnitQueue.Dequeue();
            }

            if(GenUnitQueue.Count <= 0)
            {
                Coroutine temp = UnitProductionCoroutine;
                UnitProductionCoroutine = null;
                StopCoroutine(temp);
                yield break;
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
        int sheepIndex=0;

        switch (sheep.name)
        {
            case "Unit_SheepAssasin":
                sheepIndex = 0;
                break;
            case "Unit_SheepSword":
                sheepIndex = 1;
                break;
            case "Unit_SheepJavelin":
                sheepIndex = 2;
                break;
            case "Unit_SheepBow":
                sheepIndex = 3;
                break;
            case "Unit_SheepWizard":
                sheepIndex = 4;
                break;

        }
        BM.CreateUnit(sheepIndex).transform.position = transform.position;
    }
}