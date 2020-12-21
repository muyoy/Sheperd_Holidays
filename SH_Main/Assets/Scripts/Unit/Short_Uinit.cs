﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Short_Uinit : Unit
{
    private int targetLayer;
    public GameObject attackArea;
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
        type = Type.Short;
        isMove = false;
    }
    public override void Move()
    {
        base.Move();
    }
    protected override void Attack()
    {
        base.Attack();
    }
    public void RangeAttackOn()
    {
        if (kind == Kind.Sheep)
            targetLayer = 1 << 8;
        else
            targetLayer = 1 << 9;

        Collider2D[] targets = Physics2D.OverlapBoxAll(attackArea.transform.position, attackArea.transform.localScale * 1.5f, 0, targetLayer);
        if (targets != null && atkTarget != null)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].gameObject.GetComponent<Unit>().HpChanged(atk);
                Debug.Log(targets[i].gameObject.name + " attack :" + atk);
            }
        }
    }
}
