using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowtoPlay : MonoBehaviour
{
    public GameObject[] pages;
    public GameObject preButton;
    public GameObject postButton;
    private int pageNum = 0;

    private void Start()
    {
        pages = new GameObject[transform.Find("BackGround/Pages").childCount];
        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i] == null) pages[i] = transform.Find("BackGround/Pages/Page" + i.ToString()).gameObject;
        }
        if (preButton == null) preButton = transform.Find("BackGround/PreButton").gameObject;
        if (postButton == null) postButton = transform.Find("BackGround/PostButton").gameObject;
        ButtonOnOff(pageNum);
    }
    //이전 버튼
    public void PrePage()
    {
        pages[pageNum].SetActive(false);
        pageNum--;
        pages[pageNum].SetActive(true);
        ButtonOnOff(pageNum);
    }
    //다음 버튼
    public void PostPage()
    {
        pages[pageNum].SetActive(false);
        pageNum++;
        pages[pageNum].SetActive(true);
        ButtonOnOff(pageNum);
    }

    private void ButtonOnOff(int _pageNum)
    {
        if (pageNum == 0)
        {
            preButton.SetActive(false);
            postButton.SetActive(true);
        }
        else if (pageNum != 0 && pageNum != pages.Length - 1)
        {
            preButton.SetActive(true);
            postButton.SetActive(true);
        }
        else if(pageNum == pages.Length - 1)
        {
            preButton.SetActive(true);
            postButton.SetActive(false);
        }
    }
    //설명창 닫기
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
