//************************************
//
//  EDITOR : KIM JIHUN
//  LAST UPDATE : 2020.11.17
//  Script Purpose :  GameManager
//
//***********************************

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public BattleManager BM;
    public TextMeshProUGUI sheepnum;
    public TextMeshProUGUI money_text;
    public TextMeshProUGUI day;
    public Image sunMoon;
    public int money;
    public int population;
    public int maxPopulation;

    private float time = 0.0f;

    private void Start()
    {
        money = 50;
        maxPopulation = 5;

        day.text = BM.currentWave.ToString();
        money_text.text = money.ToString();
        sheepnum.text = population.ToString() + "/" + maxPopulation.ToString();
    }

    public void DayUIChange(bool isday)
    {
        if (isday)
            StartCoroutine(Rotation(180, 0.5f));
        else
            StartCoroutine(Rotation(0, 0.5f));
    }

    private IEnumerator Rotation(float angle, float duration)
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
}
