using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum Kind { Sheep, Wolf}
    public enum Type { none, Long, Middle, Short }

    [SerializeField] protected Kind kind;
    [SerializeField] protected Type type;

    [SerializeField] protected float hp;
    protected virtual float Hp
    {
        get { return hp; }
        set
        {
            hp = value;
        }
    }
    protected Vector3 targetPos;
    protected GameObject atkTarget = null;
    protected int atk = 0;
    [SerializeField] protected float speed = 2.0f;
    protected float range = 0;
    protected float atk_cool = 0.0f;
    [SerializeField] protected bool isTarget;
    protected float gridSize = 1.28f;
    public bool isDead;
    private BattleManager BM;
    protected Rigidbody2D rb;
    protected Animator anim;

    protected virtual void Awake()
    {
        BM = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
