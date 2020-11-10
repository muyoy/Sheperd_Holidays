using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum Kind { Sheep, Wolf}
    public enum Type { Short = 384, Middle, Long }

    [SerializeField]
    protected Kind kind;
    [SerializeField]
    protected Type type;

    [SerializeField]
    protected float hp;
    protected virtual float Hp
    {
        get { return hp; }
        set
        {
            hp = value;
        }
    }

    protected Vector2 targetPos;
    protected GameObject atkTarget = null;
    protected Rigidbody2D rb;
    protected Animator ani;
    protected int atk = 0;
    protected int speed = 128;
    protected float range = 0;
    protected float atk_cool = 0.0f;

    public bool isDead;
    public BattleManager BM;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    public virtual void HpChanged(float damage)
    {
        if(!isDead)
        {
            Hp -= damage;
        }
    }

    protected virtual void Attack()
    {

    }

    protected virtual void Move()
    {
        targetPos = BM.GetWall().transform.position;
    }
}
