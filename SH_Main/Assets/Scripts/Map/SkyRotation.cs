// ==============================================================
// 하늘 회전
//
// AUTHOR: Yang SeEun
// CREATED: 2020-12-29
// UPDATED: 2020-12-29
// ==============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    private float time = 0;

    public IEnumerator Rotation(float angle, float duration)
    {
        Quaternion from = transform.rotation;
        while (time <= duration)
        {
            time += Time.deltaTime;
            //Lerp 작동안됨..
            //transform.rotation = Quaternion.Lerp(from, targetRotation, time/duration);
            transform.rotation = Quaternion.Euler(0,0, Mathf.LerpAngle(0,-angle, time / duration));
            yield return null;
        }
        time = 0;
    }
}
