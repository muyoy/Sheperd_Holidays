using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StructureInstantiate : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    #region Structure Property
    private GameObject structure = null;
    public GameObject structurePrefab;
    public Sprite prohibitStruct; // 건설 불가능시 이미지
    public Sprite accessStruct;  // 건설 가능시 이미지
    private bool canStruct;
    #endregion Structure Property

    #region Mouse Property
    private Vector2 MousePosition;
    public Canvas canvas;
    #endregion

    #region Ray Property
    private float rayDistance = 1200f;
    private Camera rayCamera;
    private RaycastHit2D hit = new RaycastHit2D();
    #endregion Ray Property

    #region UIManager
    public GameObject UIManagerGameObejct;
    private UIManager uiManager;
    #endregion

    #region GameManager
    public GameObject GameManagerGameObject;
    private GameManager gameManager;
    #endregion

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
        rayCamera = Camera.main.GetComponent<Camera>();
        GroundMask = 1 << LayerMask.NameToLayer("Ground");
        uiManager = UIManagerGameObejct.GetComponent<UIManager>();
        gameManager = GameManagerGameObject.GetComponent<GameManager>();
        Ground = uiManager.MapGetter();
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
        BuildStructure(canStruct);

        GroundSpriteInit(gameManager.IsDay);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        structure = Instantiate(structurePrefab as GameObject, MousePosition, Quaternion.identity);
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
                isEmpty.Enqueue(Ground[tileName].IsEmpty);
            }
            GroundSpriteInit(gameManager.IsDay);
            if (isEmpty.Contains(false))
            {
                // 건물의 색상 변경
                structure.GetComponent<SpriteRenderer>().sprite = prohibitStruct;

                // 타일의 색상 변경
                for (int i = tileName; i < tileName+structTile; i++)
                {
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
        for(int i = 0; i < Ground.Length; i++)
        {
            Ground[i].ground.GetComponent<SpriteRenderer>().sprite = initGround;
        }
    }

    private void BuildStructure(bool available)
    {
        if (available)
        {
            structure.transform.localPosition = hit.transform.position /* + offset */;
            structure.GetComponent<Structure>().Init();
        }
        else
        {
            Destroy(structure);

            structure = null;
        }
    }
}
