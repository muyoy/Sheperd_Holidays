﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Long_Unit : Unit
{
    private const float max_hp = 60.0f;
    private void Awake()
    {
        type = Type.Long;

        if(gameObject.layer == 9)
            kind = Kind.Sheep;
        else
            kind = Kind.Wolf;
        Init();
    }

    private void Init()
    {
        hp = max_hp;
        atk = 30;
        range = 20;
        atk_cool = 2.0f;
    }


}
