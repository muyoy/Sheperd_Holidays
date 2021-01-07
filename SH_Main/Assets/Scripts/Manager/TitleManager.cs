using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public OptionWindow optionWindow;

    void Start()
    {
        if (optionWindow == null) optionWindow = transform.Find("OptionWindow").GetComponent<OptionWindow>();
    }

    public void OptionButton()
    {
        optionWindow.OpenButton();
    }
}
