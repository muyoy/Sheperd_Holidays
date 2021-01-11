// ==============================================================
// 행성 관리 (해와 달)
//
// AUTHOR: Yang SeEun
// CREATED: 2021-01-05
// UPDATED: 2021-01-11
// ==============================================================



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [SerializeField] private float time = 0.0f;
    public float angle = 0.0f;
    public float radius = 10.0f;
    public Transform center;

    [Space(5)]
    public Sprite[] moonSprite;
    public Sprite sunSprite;
    private SpriteRenderer renderer;


    private void Awake()
    {
        center = transform.parent.transform.Find("CenterPosition").transform;
        renderer = GetComponent<SpriteRenderer>();
    }

    public void ChangedInTime(bool isMorning, int _dayTime)
    {
        if (isMorning)
        {
            renderer.sprite = sunSprite;
        }
        else
        {
            renderer.sprite = moonSprite[_dayTime / 5];
        }
    }

    #region OrbitMovement (Z축 고정)

    public IEnumerator MoveEllipse(float fromAngle, float toAngle, float duration)
    {
        float offset = Mathf.Abs(toAngle - fromAngle) / duration;
        transform.position = CalcEllipticalOrbit(fromAngle);
        while (time <= duration)
        {
            time += Time.deltaTime;
            angle = fromAngle + time * offset;
            Vector3 targetPos = CalcEllipticalOrbit(angle);

            transform.position = Vector3.MoveTowards(transform.position, targetPos, time * offset);

            yield return null;
        }
        transform.position = CalcEllipticalOrbit(toAngle);
        time = 0.0f;
    }



    public IEnumerator MoveCircle(float fromAngle, float toAngle, float duration)
    {
        float offset = Mathf.Abs(toAngle - fromAngle) / duration;
        transform.position = CalcCircularOrbit(fromAngle);

        while (time <= duration)
        {
            time += Time.deltaTime;
            angle = fromAngle + time * offset;
            Vector3 targetPos = CalcCircularOrbit(angle);

            transform.position = Vector3.MoveTowards(transform.position, targetPos, time * offset);

            yield return null;
        }
        transform.position = CalcCircularOrbit(toAngle);
        time = 0.0f;
    }


    #region 반복적으로 궤도를 따라 이동 (임시)
    private IEnumerator Move(float fromAngle)
    {
        float time = 0.0f;
        transform.position = CalcEllipticalOrbit(fromAngle);
        while (true)
        {
            time += Time.deltaTime;
            angle = fromAngle + time * speed;
            Vector3 targetPos = CalcEllipticalOrbit(angle);

            //transform.position = Vector3.Lerp(transform.position, targetPos, time * 0.02f);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.35f);

            yield return null;
        }
    }

    private float speed = 0.02f;
    private void Move_Temp()
    {
        float time = 0.0f;
        time += (Time.deltaTime) * speed;
        angle = time;

        Vector3 targetPos = CalcEllipticalOrbit(angle);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, 0.35f);
        //transform.Rotate(rotationAngles * rotationSpeed * Time.deltaTime);
    }
    #endregion



    /// <summary>
    /// 타원 궤도를 계산한다.  (x축이 긴 타원모양)
    /// </summary>
    /// <param name="_theta"></param>
    /// <returns></returns>
    private Vector3 CalcEllipticalOrbit(float _theta)
    {
        return new Vector3((center.position.x + Mathf.Sin(_theta * Mathf.Deg2Rad) * radius), (center.position.y + Mathf.Cos(_theta * Mathf.Deg2Rad) * radius * 0.5f), center.position.z);
    }

    /// <summary>
    /// 원 궤도를 계산한다.  
    /// </summary>
    /// <param name="_angle"></param>
    /// <returns></returns>
    private Vector3 CalcCircularOrbit(float _theta)
    {
        return new Vector3((center.position.x + Mathf.Sin(_theta * Mathf.Deg2Rad) * radius), (center.position.y + Mathf.Cos(_theta * Mathf.Deg2Rad) * radius), center.position.z);
    }
    #endregion
}