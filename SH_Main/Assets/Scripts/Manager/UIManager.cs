//************************************
//
//  EDITOR : KIM JIHUN
//  LAST UPDATE : 2020.12.29
//  Script Purpose :  UI Manager
//
//***********************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct GroundInfo
{
    public GameObject ground;
    public bool IsEmpty;

    public GroundInfo(GameObject gameObjectTile, bool tf)
    {
        ground = gameObjectTile;
        IsEmpty = tf;
    }
};

public class UIManager : MonoBehaviour
{
    #region TileMap
    public GameObject GroundMap;
    public GroundInfo[] Ground = new GroundInfo[45];
    public Sprite DayGround;
    public Sprite NightGround;
    #endregion TileMap

    #region UnitQueue
    public GameObject[] UnitRepIcon;
    public bool isUnitActive; // Barrack 이 활성화 되어있는 상태인가?
    #endregion UnitQueue

    #region Building
    public GameObject[] BuildingRepIcon;
    #endregion Building

    #region Bottom Interface
    public GameObject frontContent;
    private Image frontContentSprite;
    public GameObject hiddenContent;
    private Image hiddenContentSprite;
    #endregion Bottom Interface

    private UIManager uiManager;
    public OptionWindow optionWindow;
    public AsyncSceneLoad scenetransition;

    private void Awake()
    {
        if(uiManager == null)
        {
            uiManager = this;
        }
        else
        {
            Destroy(uiManager);
            uiManager = this;
        }
    }

    private void Start()
    {
        UnitRepIcon = GameObject.FindGameObjectsWithTag("UnitContent");
        BuildingRepIcon = GameObject.FindGameObjectsWithTag("BuildingContent");
        frontContentSprite = frontContent.GetComponent<Image>();
        hiddenContentSprite = hiddenContent.GetComponent<Image>();
        UnitQueueControl(); // 유닛 생산 버튼 숨기기

        if (optionWindow == null) optionWindow = transform.Find("Canvas/OptionWindow").GetComponent<OptionWindow>();
        if (scenetransition == null) scenetransition = transform.Find("Canvas/Scenetransition").GetComponent<AsyncSceneLoad>();
        optionWindow.gameObject.SetActive(false);
    }

    public void UnitQueueControl()
    {
        if (isUnitActive)
        {
            frontContentSprite.sprite = Resources.Load("UI/Unit_Button", typeof(Sprite)) as Sprite;
            hiddenContentSprite.sprite = Resources.Load("UI/Building_Button", typeof(Sprite)) as Sprite;
            for(int i = 0; i < UnitRepIcon.Length; i++) {
                UnitRepIcon[i].SetActive(true);
            }

            for(int i = 0; i < BuildingRepIcon.Length; i++)
            {
                BuildingRepIcon[i].SetActive(false);
            }

            isUnitActive = false;
        }
        else
        {
            frontContentSprite.sprite = Resources.Load("UI/Building_Button", typeof(Sprite)) as Sprite;
            hiddenContentSprite.sprite = Resources.Load("UI/Unit_Button", typeof(Sprite)) as Sprite;
            for (int i = 0; i < BuildingRepIcon.Length; i++)
            {
                BuildingRepIcon[i].SetActive(true);
            }

            for (int i = 0; i < UnitRepIcon.Length; i++)
            {
                UnitRepIcon[i].SetActive(false);
            }

            isUnitActive = true;
        }
    }

    public GroundInfo[] MapGetter() // Map 게임오브젝트에 들어있는 Image를 리스트에 삽입
    {
        for (int i = 0; i < GroundMap.transform.childCount; i++)
        {
            Ground[i] = new GroundInfo(GroundMap.transform.GetChild(i).gameObject, true);
        }

        return Ground;
    }

    public void GroundSet(bool IsDay)
    {
        if (IsDay)
        {
            for (int i = 0; i < Ground.Length; i++)// 아침이면 아침 땅 활성화
            {
                Ground[i].ground.GetComponent<SpriteRenderer>().sprite = DayGround;
            }
        }
        else
        {
            for (int i = 0; i < Ground.Length; i++)// 밤이면 아침 땅 비활성화
            {
                Ground[i].ground.GetComponent<SpriteRenderer>().sprite = NightGround;
            }
            
        }
    }

    public void OptionButton()
    {
        optionWindow.OpenButton();
        Time.timeScale = 0;
    }
    public void RestartButton()
    {
        Time.timeScale = 1;
        DBManager.instance.waveDatas.Clear();
        SoundManager.Inst.StopBgm();
        scenetransition.LoadSceneName("Sheperd Holidays");
        scenetransition.gameObject.SetActive(true);
    }

    public void ExitButton()
    {
        Time.timeScale = 1;
        DBManager.instance.waveDatas.Clear();
        SoundManager.Inst.StopBgm();
        scenetransition.LoadSceneName("TitleScene");
        scenetransition.gameObject.SetActive(true);
    }


    public void StageInit()
    {

    }

}
