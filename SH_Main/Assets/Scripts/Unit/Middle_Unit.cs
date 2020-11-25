﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Middle_Unit : Unit
{
    public GameObject projectile;
    private const float max_hp = 60.0f;
    protected override void Awake()
    {
        base.Awake();
        Init();
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Init()
    {
        type = Type.Middle;
        hp = max_hp;
        atk = 30;
        range = 3 * gridSize;
        atk_cool = 2.0f;
        isMove = true;
    }

    protected override void Attack()
    {
        anim.SetTrigger(HashCode.AttackID);
    }
}
