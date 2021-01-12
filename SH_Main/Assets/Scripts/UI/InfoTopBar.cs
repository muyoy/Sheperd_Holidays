// ==============================================================
// 상단 정보 UI
//
// AUTHOR: Yang SeEun
// CREATED: 2021-01-11
// UPDATED: 2021-01-12
// ==============================================================

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class InfoTopBar : MonoBehaviour
{
    private GameManager gameManager;
    private BattleManager battleManager;

    public TextMeshProUGUI wave_Label;
    public TextMeshProUGUI sheep_Label;
    public TextMeshProUGUI money_text;

    public float duration = 2f;
    private int m_curPopulation;
    private int m_curMoney;
    private IEnumerator Co_SheepCount = null;
    private IEnumerator Co_MoneyCount = null;

    public Image sunMoon;
    private float time = 0.0f;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
    }

    private void Start()
    {
        Init(battleManager.currentWave, gameManager.population, gameManager.money);

        //예시
        //ChangeSheepLabel(4, 0);
        //ChangeMoneyLabel(1500, 1200);
    }

    private void Init(int _wave, int _population, int _money)
    {
        wave_Label.text = string.Format("{0}", _wave);
        sheep_Label.text = string.Format("{0}", _population);
        money_text.text = string.Format("{0}", _money);

        m_curPopulation = _population;
        m_curMoney = _money;
    }

    #region 하늘 이미지 회전
    public void DayUIChange(bool isday)
    {
        if (isday)
            StartCoroutine(SkyRotation(180, 0.5f));
        else
            StartCoroutine(SkyRotation(0, 0.5f));
    }

    private IEnumerator SkyRotation(float angle, float duration)
    {
        float fromAngle = sunMoon.rectTransform.localEulerAngles.z;
        float offset = Mathf.Abs(angle - fromAngle) / duration;
        while (time <= duration)
        {
            time += Time.deltaTime;
            sunMoon.rectTransform.Rotate(Vector3.back, offset * Time.deltaTime);
            yield return null;
        }
        sunMoon.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
        time = 0;
    }
    #endregion

    #region 웨이브
    public void ChangeWaveLabel(int target)
    {
        wave_Label.text = string.Format("{0}", target);
    }
    #endregion

    #region 양 인구수
    public void ChangeSheepLabel(int _curAmount, int _targetAmount)
    {
        if(Co_SheepCount != null)
        {
            StopCoroutine(Co_SheepCount);
        }
        else
        {
            m_curPopulation = _curAmount;
        }

        Co_SheepCount = SheepCount(_targetAmount);
        StartCoroutine(Co_SheepCount);
    }

    private IEnumerator SheepCount(int target)
    {
        float offest = (target - m_curPopulation) / duration;
        target = Mathf.Clamp(target, 0, gameManager.maxPopulation);


        if (target - m_curPopulation > 0) //증가
        {
            while (m_curPopulation < target)
            {
                sheep_Label.text = string.Format("{0}/{1}", m_curPopulation, gameManager.maxPopulation);
                m_curPopulation += Mathf.CeilToInt(offest * Time.deltaTime);
                yield return null;
            }
        }
        else  //감소
        {
            offest = Mathf.Abs(offest);
            while (m_curPopulation > target)
            {
                sheep_Label.text = string.Format("{0}/{1}", m_curPopulation, gameManager.maxPopulation);
                m_curPopulation -= Mathf.CeilToInt(offest * Time.deltaTime);
                yield return null;
            }
        }

        sheep_Label.text = string.Format("{0}/{1}", target, gameManager.maxPopulation);
        m_curPopulation = target;

        Co_SheepCount = null;
        yield return null;

    }

    #endregion

    #region 돈(짚)
    public void ChangeMoneyLabel(int _curAmount, int _targetAmount)
    {
        if (Co_MoneyCount != null)
        {
            StopCoroutine(Co_MoneyCount);
        }
        else
        {
            m_curMoney = _curAmount;
        }

        Co_MoneyCount = MoneyCount(_targetAmount);
        StartCoroutine(Co_MoneyCount);
    }

    private IEnumerator MoneyCount(int target)
    {
        float offest = (target - m_curMoney) / duration;
        target = Mathf.Clamp(target, 0, 9999);


        if (target - m_curMoney > 0) //증가
        {
            while (m_curMoney < target)
            {
                money_text.text = string.Format("{0}", m_curMoney);
                m_curMoney += Mathf.CeilToInt(offest * Time.deltaTime);
                yield return null;
            }
        }
        else  //감소
        {
            offest = Mathf.Abs(offest);
            while (m_curMoney > target)
            {
                money_text.text = string.Format("{0}", m_curMoney);
                m_curMoney -= Mathf.CeilToInt(offest * Time.deltaTime);
                yield return null;
            }
        }

        money_text.text = string.Format("{0}", target);
        m_curMoney = target;

        Co_MoneyCount = null;
        yield return null;

    }
    #endregion
}
