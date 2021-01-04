// ==============================================================
// 카메라 이동 제어
// 
//
// AUTHOR: Yang SeEun
// CREATED: 2020-12-08
// UPDATED: 2021-01-04
// ==============================================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] private bool MovingFromMouse = true;
    public float offset = 300.0f;
    public float speed = 2.0f;

    private Vector3 curMousePos;
    private Vector3 cameraMove;

    private float screenWidth;
    private float screenHeight;

    //auto
    public bool autoMoving = false;
    [SerializeField] private Vector3 startingPoint;
    private IEnumerator Cor_autoMovement = null;

    private void Awake()
    {
        startingPoint = transform.position;
    }

    private void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        //Cursor.lockState = CursorLockMode.Confined;
    }

    private void Clamp()
    {

    }

    private void FixedUpdate()
    {
        #region 테스트용
        if (Input.GetKeyDown(KeyCode.T) && Cor_autoMovement == null)
        {
            Cor_autoMovement = AutoMovement(startingPoint, 2.0f, 3.0f);
            StartCoroutine(Cor_autoMovement);
        }

        #endregion

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
                    cameraMove = Vector3.right * h * speed * 2 * Time.deltaTime;
                    transform.position += cameraMove;
                    return;
                }
            }

            //마우스 커서로 이동
            CursorMovement();
        }
    }
    private void CursorMovement()
    {
        if (!EventSystem.current.IsPointerOverGameObject()) //포인터 위치에 UI가 없다면
        {
            curMousePos = Input.mousePosition;
            if ((curMousePos.x > screenWidth - offset) && curMousePos.x < screenWidth)      //Right
            {
                cameraMove = Vector3.right * speed * Time.deltaTime;
                transform.position += cameraMove;
                MovingFromMouse = true;
                return;
            }
            else if ((curMousePos.x < offset) && curMousePos.x > 0)         //Left
            {
                cameraMove = Vector3.left * speed * Time.deltaTime;
                transform.position += cameraMove;
                MovingFromMouse = true;
                return;
            }
            else
            {
                MovingFromMouse = false;
            }
        }
    }

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