using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Short_Uinit : Unit
{
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
        type = Type.Short;
        hp = max_hp;
        atk = 30;
        range = 1.5f * gridSize;
        atk_cool = 2.0f;
        isMove = true;
    }
    protected override void Work()
    {
        base.Work();

    }
    protected override void Attack()
    {
        base.Attack();
        //anim.SetBool(HashCode.AttackID, true);
    }
}
