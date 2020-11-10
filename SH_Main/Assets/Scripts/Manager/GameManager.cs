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


public class GameManager : MonoBehaviour
{
    private bool IsDay; // 아침인지 밤인지 구분하는 bool
    public GameObject MorningMap; // 아침 맵
    public GameObject EveningMap; // 저녁 맵
    private List<GroundInfo> morningGround;
    private List<GroundInfo> eveningGround;

    private void Awake()
    {
        morningGround = new List<GroundInfo>();
        eveningGround = new List<GroundInfo>();

        IsDay = false; // 밤으로 게임 시작
    }

    private void Start()
    {

    }

    private void MapGetter() // Map 게임오브젝트에 들어있는 Image를 리스트에 삽입
    {
        for (int i = 0; i < MorningMap.transform.childCount; i++)
        {
            morningGround.Add(new GroundInfo(MorningMap.transform.GetChild(i).GetComponent<Image>(), true));
        }
        for (int i = 0; i < EveningMap.transform.childCount; i++)
        {
            eveningGround.Add(new GroundInfo(EveningMap.transform.GetChild(i).GetComponent<Image>(), true));
        }
    }

    private void GroundSet()
    {
        if (IsDay)
        {
            for (int i = 0; i < morningGround.Count; i++)// 아침이면 아침 땅 활성화
            {
                morningGround[i].ground.gameObject.SetActive(true);
            }
            for (int i = 0; i < eveningGround.Count; i++)
            {
                eveningGround[i].ground.gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < morningGround.Count; i++)// 밤이면 아침 땅 비활성화
            {
                morningGround[i].ground.gameObject.SetActive(false);
            }
            for (int i = 0; i < eveningGround.Count; i++)
            {
                eveningGround[i].ground.gameObject.SetActive(true);
            }
        }
    }

    
}
