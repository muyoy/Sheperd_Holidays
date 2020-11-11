using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public bool IsDay; // 아침인지 밤인지 구분하는 bool
    

    private void Awake()
    {
        

        IsDay = false; // 밤으로 게임 시작
    }

    private void Start()
    {

    }

    

    

    
}
