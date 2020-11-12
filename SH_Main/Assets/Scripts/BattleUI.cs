using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    private bool isDay; // 아침인지 밤인지 구분하는 bool
    public bool IsDay
    {
        get { return isDay; }
        set
        {
            isDay = value;
            uiManager.GroundSet(IsDay);
        }
    }

    private List<GroundInfo> dayGround = new List<GroundInfo>();
    private List<GroundInfo> nightGround = new List<GroundInfo>();

    public GameObject uiGameObject;
    private UIManager uiManager;

    private float rayDistance;
    private Vector2 MousePosition;
    Camera rayCamera;
    private RaycastHit hit = new RaycastHit();

    private int StructureMask;
    private GameObject structure = null;

    private int GroundMask;


    private void Awake()
    {
        uiManager = uiGameObject.GetComponent<UIManager>();
        dayGround = uiManager.dayGround;
        nightGround = uiManager.nightGround;

        rayCamera = Camera.main.GetComponent<Camera>();
    }

    private void Start()
    {
        StructureMask = 1 << LayerMask.NameToLayer("Structure");
        GroundMask = 1 << LayerMask.NameToLayer("Ground");
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ShootRay();

        }
    }

    private void ShootRay()
    {
        MousePosition = Input.mousePosition;
        MousePosition = rayCamera.ScreenToWorldPoint(MousePosition);
        Ray ray = rayCamera.ScreenPointToRay(MousePosition);

        // 생성된 건물이 마우스 포인터를 따라다님 offset 계산 필요
        structure.transform.position = MousePosition;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, rayDistance, GroundMask))
        {
            if (hit.transform.CompareTag("Grid"))
            {
                

            }
        }
    }
}
