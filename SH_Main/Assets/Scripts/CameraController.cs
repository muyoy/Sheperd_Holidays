using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float offset = 300.0f;
    public float speed = 2.0f;

    private Vector3 curMousePos;
    private Vector3 cameraMove;

    private float screenWidth;
    private float screenHeight;

    public bool autoMoving = false;
    [SerializeField] private Vector3 startingPoint;
    private IEnumerator Cor_movement = null;

    private void Awake()
    {
        startingPoint = transform.position;
    }

    private void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    [SerializeField] private float arrowDuration = 10.0f;
    [SerializeField] private float mouseDuration = 10.0f;
    [SerializeField] private bool MovingFromMouse = true;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Cor_movement == null)
        {
            Cor_movement = Movement(startingPoint, 2.0f);
            StartCoroutine(Cor_movement);
        }

        if (Input.GetKeyDown(KeyCode.T) && Cor_movement == null)
        {
            Cor_movement = Movement(startingPoint, 2.0f, 3.0f);
            StartCoroutine(Cor_movement);
        }

        if (autoMoving.Equals(false))
        {
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

    private IEnumerator Movement(Vector3 targetPos, float speed, float duration)
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
        Cor_movement = null;
    }

    private IEnumerator Movement(Vector3 targetPos, float speed)
    {
        autoMoving = true;

        while ((targetPos - transform.position).magnitude > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        autoMoving = false;
        Cor_movement = null;
    }
}