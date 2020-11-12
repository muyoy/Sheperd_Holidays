using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct GroundInfo
{
    public Image ground;
    public bool IsEmpty;

    public GroundInfo(Image image, bool tf)
    {
        ground = image;
        IsEmpty = tf;
    }
};

public class UIManager : MonoBehaviour
{
    public GameObject DayMap; // 아침 맵
    public GameObject NightMap; // 저녁 맵
    public List<GroundInfo> dayGround;
    public List<GroundInfo> nightGround;

    private UIManager uiManager;

    private void Awake()
    {
        dayGround = new List<GroundInfo>();
        nightGround = new List<GroundInfo>();

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

    private void MapGetter() // Map 게임오브젝트에 들어있는 Image를 리스트에 삽입
    {
        for (int i = 0; i < DayMap.transform.childCount; i++)
        {
            dayGround.Add(new GroundInfo(DayMap.transform.GetChild(i).GetComponent<Image>(), true));
        }
        for (int i = 0; i < NightMap.transform.childCount; i++)
        {
            nightGround.Add(new GroundInfo(NightMap.transform.GetChild(i).GetComponent<Image>(), true));
        }
    }

    public void GroundSet(bool IsDay)
    {
        if (IsDay)
        {
            for (int i = 0; i < dayGround.Count; i++)// 아침이면 아침 땅 활성화
            {
                dayGround[i].ground.gameObject.SetActive(true);
            }
            for (int i = 0; i < nightGround.Count; i++)
            {
                nightGround[i].ground.gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < dayGround.Count; i++)// 밤이면 아침 땅 비활성화
            {
                dayGround[i].ground.gameObject.SetActive(false);
            }
            for (int i = 0; i < nightGround.Count; i++)
            {
                nightGround[i].ground.gameObject.SetActive(true);
            }
        }
    }

    public void StageInit()
    {

    }
}
