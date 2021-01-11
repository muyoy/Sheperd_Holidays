using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Long_Unit : Unit
{
    public GameObject projectile;
    public Transform weaponPos;
    private const float max_hp = 60.0f;
    private const float proj_speed = 2.0f;

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
        type = Type.Long;
        isMove = false;
    }
    public override void Move()
    {
        base.Move();
    }
    public void AttackOn_RangeProj()
    {
        Vector2 direction;
        GameObject obj = Instantiate(projectile, weaponPos.position, Quaternion.Euler(0.0f, 0.0f, -90.0f));
        obj.GetComponent<RangeProjectile>().SetAtk(atk);
        if (kind == Kind.Sheep)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
        obj.GetComponent<Rigidbody2D>().AddForce(direction * proj_speed * 100.0f, ForceMode2D.Force);
    }
    protected override void Attack()
    {
        base.Attack();
        //anim.SetBool(HashCode.AttackID, true);
    }
}
