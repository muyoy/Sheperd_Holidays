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
    [SerializeField] protected float speed = 1.7f;
    protected float range = 0;
    protected float atk_cool = 0.0f;
    protected float tab = 0.0f;
    [SerializeField] protected bool isTarget;
    protected float gridSize = 1.28f;
    public bool isDead;
    public bool isMove;
    protected Rigidbody2D rb;
    protected Animator anim;
    private Coroutine walk = null;

    private BattleManager bm;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bm = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();

        if (gameObject.layer == 9)
            kind = Kind.Sheep;
        else
            kind = Kind.Wolf;
    }
    protected virtual void Start()
    {
        //StartCoroutine(StartOn());
    }
    public virtual void HpChanged(float damage)
    {
        if(!isDead)
        {
            Hp -= damage;
        }
    }
    public void SetTarget(GameObject obj)
    {
        atkTarget = obj;
    }
    public IEnumerator StartOn()
    {
        yield return new WaitForSeconds(2.0f);
        Work();
        Move();
    }

    protected virtual void Init() {   }
    protected virtual void Attack()
    {
        anim.SetBool(HashCode.AttackID, true);
    }

    public virtual void AttackOn()
    {
        if (atkTarget != null)
        {
            Debug.Log("Attack! " + atk);
            atkTarget.GetComponent<Unit>().HpChanged(atk);
        }
    }
    protected virtual void Work()
    {
        StartCoroutine(AttackCheck());
    }
    protected virtual void Move()
    {
        if (kind == Kind.Sheep)
        {
            targetPos += new Vector3((int)type * gridSize, 0.0f, 0.0f);
            tab = Random.Range(-0.3f, 0.3f);
            targetPos += new Vector3(tab, 0.0f, 0.0f);
            if (isMove)
            {
                walk = StartCoroutine(SheepWalk(targetPos));
            }
            else
            {
                StopCoroutine(walk);
                walk = StartCoroutine(SheepWalk(targetPos));
            }
        }
        else
        {
            walk = StartCoroutine(WolfWalk());
        }
    }

    public void GetPosition(GameObject point)
    {
        targetPos = point.transform.position;
    }

    private IEnumerator SheepWalk(Vector3 _targetPos)
    {
        isMove = false;
        anim.SetBool(HashCode.walkID, true);
        while ((_targetPos.x - transform.position.x) >= 0)
        {
            if(transform.position.x > bm.GetWall().transform.position.x)
            {
                bm.ReTargetWolf();
            }
            rb.position += Vector2.right * speed * Time.deltaTime;
            yield return null;
        }
        anim.SetBool(HashCode.walkID, false);
        isMove = true;
    }
    private IEnumerator WolfWalk()
    {
        isMove = false;
        anim.SetBool(HashCode.walkID, true);
        while (!isTarget)
        {
            rb.position += Vector2.left * speed * Time.deltaTime;

            yield return null;
        }
        anim.SetBool(HashCode.walkID, false);
        isMove = true;
    }
    private IEnumerator AttackCheck()
    {
        while (!isDead)
        {           
            if (atkTarget != null && Mathf.Abs(atkTarget.transform.position.x - transform.position.x) <= range)
            {
                if (isMove == false)
                {
                    StopCoroutine(walk);
                }
                Attack();
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
}
