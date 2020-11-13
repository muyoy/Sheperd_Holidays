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
    public GameObject GroundMap;
    public GroundInfo[] Ground = new GroundInfo[45];
    public Sprite DayGround;
    public Sprite NightGround;

    private UIManager uiManager;

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

    public void StageInit()
    {

    }
}
