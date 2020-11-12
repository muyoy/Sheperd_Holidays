using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
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

    private List<GroundInfo> dayGround;
    private List<GroundInfo> nightGround;

    public GameObject uiGameObject;
    private UIManager uiManager;
    


    private void Awake()
    {
        uiManager = uiGameObject.GetComponent<UIManager>();
        dayGround = uiManager.dayGround;
        nightGround = uiManager.nightGround;
    }

    private void Start()
    {

    }

    private void StageInit()
    {
        IsDay = true; // 밤으로 게임 시작
        uiManager.StageInit();

    }
    
}
