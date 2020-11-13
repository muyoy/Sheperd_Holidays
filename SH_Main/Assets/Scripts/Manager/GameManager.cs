using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool isDay = true;
    public bool IsDay
    {
        get { return isDay; }
        set { isDay = value; }
    }
}
