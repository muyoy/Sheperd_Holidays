using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public GameObject[] pages;
    public GameObject preButton;
    public GameObject postButton;
    public GameObject pressplay;
    public AsyncSceneLoad scenetransition;
    private int pageNum = 0;

    private void Start()
    {
        if (scenetransition == null) scenetransition = transform.Find("Scenetransition").GetComponent<AsyncSceneLoad>();
        pages = new GameObject[transform.Find("Cartoon").childCount];
        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i] == null) pages[i] = transform.Find("Cartoon/" + i.ToString()).gameObject;
        }
        if (preButton == null) preButton = transform.Find("PreButton").gameObject;
        if (postButton == null) postButton = transform.Find("PostButton").gameObject;
        if (pressplay == null) pressplay = transform.Find("Play").gameObject;
        scenetransition.gameObject.SetActive(false);
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
        else if (pageNum == pages.Length - 1)
        {
            preButton.SetActive(true);
            postButton.SetActive(false);
            pressplay.SetActive(true);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && pressplay.activeSelf)
        {
            scenetransition.LoadSceneName("Sheperd Holidays");
            scenetransition.gameObject.SetActive(true);
            SoundManager.Inst.StopBgm();
        }
    }
}
