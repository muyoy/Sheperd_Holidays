// ==============================================================
// 하늘 회전
//
// AUTHOR: Yang SeEun
// CREATED: 2020-12-29
// UPDATED: 2021-01-06
// ==============================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    public float time = 0;

    public IEnumerator Rotation(float angle, float duration)
    {
        //Time.timeScale = 0.1f;
        float fromAngle = transform.localEulerAngles.z;
        float offset = Mathf.Abs(angle - fromAngle)  / duration;
        while (time <= duration)
        {
            time += Time.deltaTime;
            transform.Rotate(Vector3.back, offset * Time.deltaTime);

            #region 여러방법 시도
            //transform.rotation = Quaternion.Lerp(from, targetRotation, time/duration);                                    왜인지 모르지만 Lerp 작동 안됨
            //transform.rotation = Quaternion.Euler(0, 0, Mathf.LerpAngle(fromAngle, angle, time / duration));              짐벌락때문에 180도 이상 회전시 가까운 방향으로 회전..
            #endregion
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, angle);
        time = 0;
    }
}
