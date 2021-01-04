//*********************************************************************************************
//
//  EDITOR : KIM JIHUN
//  LAST UPDATE : 2020.11.18
//  Script Purpose :  Manage structure instantiating UI, Check Probabilty to build structures
//                    - Dragging Structure from UI
//                    - Change Sprites when we can or can't build structure on the tiles
//
//*********************************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StructureInstantiate : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerUpHandler
{
    #region Structure Property
    private Structure structureProp; // Structure 프리팹에 존재하는 Structure 프로퍼티
    private GameObject structure = null; // 건물 드래그시 임시로 나오는 건물 이미지
    public GameObject structurePrefab;
    public Sprite prohibitStruct; // 건설 불가능시 이미지
    public Sprite accessStruct;  // 건설 가능시 이미지
    private bool canStruct;
    private Vector3 offset; // 건물 칸수에 따른 설치 위치 조정
    private const float UNIT = 1.28f; // 건물 1칸의 거리
    #endregion Structure Property

    #region Mouse Property
    private Vector2 MousePosition;
    private Canvas canvas;
    #endregion

    #region Ray Property
    private float rayDistance = 1200f;
    private Camera rayCamera;
    private RaycastHit2D hit = new RaycastHit2D();
    #endregion Ray Property

    #region Manager
    private UIManager uiManager;
    private BattleManager BattleManager;
    #endregion Manager

    #region Ground Property
    public int GroundMask;
    private GroundInfo[] Ground;
    public Sprite accessGround; // 건설 가능시 이미지
    public Sprite prohibitGround; // 건설 불가능시 이미지
    public Sprite dayGround;
    public Sprite nightGround;
    private Sprite initGround; // 초기화 할 때 필요한 타일의 이미지
    #endregion GroundProperty

    private void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        uiManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        BattleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        rayCamera = Camera.main.GetComponent<Camera>();

        GroundMask = 1 << LayerMask.NameToLayer("Ground");
        Ground = uiManager.MapGetter();
        structureProp = structurePrefab.GetComponent<Structure>();
    }

    private void Start()
    {
        offset = new Vector3(UNIT * structureProp.buildingSpace / 2 - 0.64f, 1.06f /* 건물 위로 올리기 */ , structurePrefab.transform.position.z);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        structure.transform.position += new Vector3(eventData.delta.x / (canvas.scaleFactor * 100f), eventData.delta.y / (canvas.scaleFactor * 100f), 0);
        ShootRay();
        canStruct = CheckBuildSpace();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //BuildStructure(canStruct);

        GroundSpriteInit(BattleManager.isDay);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        structure = Instantiate(structurePrefab, MousePosition, Quaternion.identity);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ShootRay();
        canStruct = CheckBuildSpace();
        BuildStructure(canStruct);
        GroundSpriteInit(BattleManager.isDay);
    }

    private void ShootRay()
    {
        MousePosition = rayCamera.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);

        hit = Physics2D.Raycast(ray.origin, ray.direction, rayDistance, GroundMask);
    }

    private bool CheckBuildSpace()
    {
        int tileName;
        int structTile;
        Queue<bool> isEmpty = new Queue<bool>();

        if (hit.transform != null)
        {
            // GameObject의 이름이 순서대로 0일 경우에만 가능
            int.TryParse(hit.transform.gameObject.name, out tileName);

            structTile = structure.GetComponent<Structure>().buildingSpace;

            // 큐에 각 타일들의 IsEmpty bool 값을 넣어 건설 가능 지역인지 판단.
            for (int i = 0; i < structTile; i++)
            {
                if (tileName + i >= Ground.Length)
                {
                    Debug.Log("Ground Length Out of Range");
                    isEmpty.Enqueue(false);
                    break;
                }
                else
                {
                    isEmpty.Enqueue(Ground[tileName + i].IsEmpty);
                }
            }
            GroundSpriteInit(BattleManager.isDay);
            if (isEmpty.Contains(false))
            {
                // 건물의 색상 변경
                structure.GetComponent<SpriteRenderer>().sprite = prohibitStruct;

                // 타일의 색상 변경
                for (int i = tileName; i < tileName + structTile; i++)
                {
                    if (i >= Ground.Length) // 제일 오른쪽에 건물을 놓았을 때 설치 불가능한 예외 처리
                    {
                        break;
                    }

                    if (Ground[i].IsEmpty)
                    {
                        Ground[i].ground.GetComponent<SpriteRenderer>().sprite = accessGround;
                    }
                    else
                    {
                        Ground[i].ground.GetComponent<SpriteRenderer>().sprite = prohibitGround;
                    }
                }

                return false;  // 마우스를 땟을 때 건설 불가능
            }
            else
            {
                // 건물의 색상 변경
                structure.GetComponent<SpriteRenderer>().sprite = accessStruct;

                // 타일의 색상 변경
                for (int i = tileName; i < tileName + structTile; i++)
                {
                    if (Ground[i].IsEmpty)  // 혹시 모르니 isEmpty 체크
                    {
                        Ground[i].ground.GetComponent<SpriteRenderer>().sprite = accessGround;
                    }
                }

                return true;  // 마우스를 땟을 때 건설 가능
            }
        }
        else
        {
            structure.GetComponent<SpriteRenderer>().sprite = prohibitStruct;
            return false;
        }
    }

    private void GroundSpriteInit(bool isDay)
    {
        if (isDay)
        {
            initGround = dayGround;
        }
        else
        {
            initGround = nightGround;
        }
        for (int i = 0; i < Ground.Length; i++)
        {
            Ground[i].ground.GetComponent<SpriteRenderer>().sprite = initGround;
        }
    }


    // 건물 위치에 맞게 재배치
    private void BuildStructure(bool available)
    {
        if (available)
        {
            structure.transform.SetParent(hit.transform, false);
            structure.transform.localPosition = offset;

            int tileName;
            int.TryParse(hit.transform.name, out tileName);

            for (int i = tileName; i < tileName + structureProp.buildingSpace; i++)
            {
                Ground[i].IsEmpty = false;
            }

            // 건물의 기능 본격적으로 시작
            structure.GetComponent<Structure>().Init();
        }
        else
        {
            Destroy(structure);
            structure = null;
        }
    }


}
