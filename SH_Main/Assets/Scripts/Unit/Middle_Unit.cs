using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Middle_Unit : Unit
{
    public GameObject projectile;
    public Transform weaponPos;
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
        base.Attack();
        //GameObject obj = Instantiate(projectile, weaponPos.position, Quaternion.Euler(0.0f, 0.0f, -90.0f));
        //anim.SetBool(HashCode.AttackID, true);
    }
}
