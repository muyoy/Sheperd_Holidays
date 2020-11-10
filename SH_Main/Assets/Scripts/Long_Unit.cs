using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Long_Unit : Unit
{
    private const float max_hp = 60.0f;
    protected override void Awake()
    {
        base.Awake();
        type = Type.Long;

        if(gameObject.layer == 9)
            kind = Kind.Sheep;
        else
            kind = Kind.Wolf;
    }
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        hp = max_hp;
        atk = 30;
        range = 20;
        atk_cool = 2.0f;
        Move();
    }

    protected override void Move()
    {
        base.Move();
        targetPos += new Vector2(128.0f, 0.0f);
        transform.position = targetPos;
    }

    protected override void Attack()
    {
        
    }

}
