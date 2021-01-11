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
    public void AttackOn()
    {
        if (kind == Kind.Sheep)
        {
            targetLayer = 1 << 8;
            Collider2D targets = Physics2D.OverlapBox(attackArea.transform.position, attackArea.transform.localScale * 1.5f, 0, targetLayer);
            if (targets != null && atkTarget != null)
            {
                targets.gameObject.GetComponent<Unit>().HpChanged(atk);
            }
        }
        else
        {
            targetLayer = 1 << 9 | 1 << 10;
            Collider2D targets = Physics2D.OverlapBox(attackArea.transform.position, attackArea.transform.localScale * 1.5f, 0, targetLayer);
            if (targets != null && atkTarget != null)
            {
                if (targets.gameObject.layer == 9)
                    targets.gameObject.GetComponent<Unit>().HpChanged(atk);
                else
                    targets.gameObject.GetComponent<Structure>().HpChange(atk);
            }
        }
    }
    public void RangeAttackOn()
    {
        if (kind == Kind.Sheep)
        {
            targetLayer = 1 << 8;
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
        else
        {
            targetLayer = 1 << 9 | 1 << 10;
            Collider2D[] targets = Physics2D.OverlapBoxAll(attackArea.transform.position, attackArea.transform.localScale * 1.5f, 0, targetLayer);
            if (targets != null && atkTarget != null)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    Debug.Log(targets[i].gameObject.name + " attack :" + atk);
                    if (targets[i].gameObject.layer == 9)
                        targets[i].gameObject.GetComponent<Unit>().HpChanged(atk);
                    else
                        targets[i].gameObject.GetComponent<Structure>().HpChange(atk);
                }
            }
        }
    }
}