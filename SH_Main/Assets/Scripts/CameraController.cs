// ==============================================================
// 카메라 이동 제어
//
// AUTHOR: Yang SeEun
// CREATED: 2020-12-08
// UPDATED: 2021-01-05
// ==============================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    //스크린 사이즈
    private float screenWidth;
    private float screenHeight;

    //카메라 이동 범위
    private float xMin = 8.96f;
    private float xMax = 47.0f;

    public float speed = 3.0f;

    //마우스로 카메라 이동
    [SerializeField] private bool MovingFromMouse = true;       //마우스로 움직이고 있는지
    public float offset = 300.0f;                               //마우스 이동범위

    private Vector3 curMousePos;                                //현재 마우스위치
    private Vector3 cameraMoveDir;                              //카메라 이동방향


    //자동이동
    public bool autoMoving = false;                             //자동 이동 중인지
    [SerializeField] private Vector3 startingPoint;             //카메라 처음 시작 위치
    private IEnumerator Cor_autoMovement = null;              

    private void Awake()
    {
        startingPoint = transform.position;
    }

    private void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        //TODO: 잠시 주석 (다시 활성화 해야함!)
        //Cursor.lockState = CursorLockMode.Confined;
    }

    /// <summary>
    /// 이동 제한걸기 (x축만)
    /// </summary>
    /// <returns></returns>
    private Vector3 ClampMove(Vector3 _cameraMoveDir)
    {
        Vector3 curPos = transform.position + _cameraMoveDir;

        float x = Mathf.Clamp(curPos.x, xMin, xMax);
        return new Vector3(x, transform.position.y, transform.position.z);
    }


    private void FixedUpdate()
    {
#if UNITY_EDITOR
        #region 테스트용
        if (Input.GetKeyDown(KeyCode.T) && Cor_autoMovement == null)
        {
            Cor_autoMovement = AutoMovement(startingPoint, 2.0f, 3.0f);
            StartCoroutine(Cor_autoMovement);
        }

        #endregion
#endif
        if (Input.GetKeyDown(KeyCode.Space) && Cor_autoMovement == null)
        {
            Cor_autoMovement = AutoMovement(startingPoint, 2.0f);
            StartCoroutine(Cor_autoMovement);
        }

        if (autoMoving.Equals(false))
        {
            //키보드 입력으로 이동
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                if (MovingFromMouse.Equals(false))
                {
                    //TODO: 양쪽키 동시에 눌렀을때 먼저 누른키만 작동하게 할것!
                    // 버그 (현재 동시입력시 h =0으로 멈춤)
                    //
                    float h = Input.GetAxisRaw("Horizontal");
                    cameraMoveDir = Vector3.right * h * speed * 2 * Time.deltaTime;

                    transform.position = ClampMove(cameraMoveDir);
                    return;
                }
            }

            CursorMovement();
        }
    }



    /// <summary>
    /// 마우스 커서로 카메라 이동
    /// </summary>
    private void CursorMovement()
    {
        if (!EventSystem.current.IsPointerOverGameObject())         //포인터 위치에 UI가 없다면
        {
            curMousePos = Input.mousePosition;
            if ((curMousePos.x > screenWidth - offset) && curMousePos.x < screenWidth)      //Right
            {
                cameraMoveDir = Vector3.right * speed * Time.deltaTime;
                    transform.position = ClampMove(cameraMoveDir);
                MovingFromMouse = true;
                return;
            }
            else if ((curMousePos.x < offset) && curMousePos.x > 0)         //Left
            {
                cameraMoveDir = Vector3.left * speed * Time.deltaTime;
                    transform.position = ClampMove(cameraMoveDir);
                MovingFromMouse = true;
                return;
            }
            else
            {
                MovingFromMouse = false;
            }
        }
    }






    /// <summary>
    /// 자동으로 카메라 이동
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="speed"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator AutoMovement(Vector3 targetPos, float speed, float duration)
    {
        autoMoving = true;
        float offest = (targetPos - transform.position).magnitude / duration;

        while ((targetPos - transform.position).magnitude > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, offest * speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        autoMoving = false;
        Cor_autoMovement = null;
    }

    private IEnumerator AutoMovement(Vector3 targetPos, float speed)
    {
        autoMoving = true;

        while ((targetPos - transform.position).magnitude > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        autoMoving = false;
        Cor_autoMovement = null;
    }
}