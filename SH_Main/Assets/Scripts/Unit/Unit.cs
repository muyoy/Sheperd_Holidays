using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum Kind { Sheep, Wolf}
    public enum Type { none, Long, Middle, Short }
    public enum AtkType { Single, Multiple }
    [SerializeField] public Kind kind;
    [SerializeField] public Type type;
    [SerializeField] public AtkType Atktype;

    [SerializeField] protected float hp;
    protected virtual float Hp
    {
        get { return hp; }
        set
        {
            hp = value;
            //hp = Mathf.Clamp(value, 0, maxHp);

            if (!isDead && hp <= 0)
            {
                hp = -1;
                Dead();
            }
        }
    }
    protected Vector3 targetPos;
    protected GameObject atkTarget = null;

    protected float range = 0.0f;
    protected float maxHp = 0.0f;
    [SerializeField] protected float atk = 0.0f;
    protected float rangeAtk = 0.0f;
    protected float atk_cool = 0.0f;
    [SerializeField] protected float movespeed = 0.0f;

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

    }
    public IEnumerator StartOn()
    {
        if (kind == Kind.Sheep)
        {
            yield return new WaitForSeconds(2.0f);
        }
        else
        {
            yield return null;
        }
        StartCoroutine(AttackCheck());
        Move();
    }
    protected virtual void Init() {   }

    public void SetUnitData(DBStruct.SheepData sheepData)
    {
        maxHp = sheepData.hp;
        hp = maxHp;
        float atkRange = sheepData.atkRange;
        range = atkRange * gridSize;
        atk = sheepData.atk;
        rangeAtk = sheepData.rangeAtk;
        atk_cool = sheepData.atkDelay;
        movespeed = sheepData.moveSpeed;
        Atktype = (AtkType)sheepData.type;
    }
    public virtual void Move()
    {
        if (kind == Kind.Sheep)
        {
            targetPos += new Vector3((int)type * gridSize, 0.0f, 0.0f);
            tab = Random.Range(-0.3f, 0.3f);
            targetPos += new Vector3(tab, 0.0f, 0.0f);
            if (!isMove)
            {
                walk = StartCoroutine(SheepWalk(targetPos));
            }
            else
            {
                StopCoroutine(walk);
                anim.SetBool(HashCode.walkID, false);
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
        isMove = true;
        anim.SetBool(HashCode.walkID, true);
        while ((_targetPos.x - transform.position.x) >= 0)
        {
            if(transform.position.x > bm.GetWall().transform.position.x)
            {
                bm.ReTargetWolf();
            }
            rb.position += Vector2.right * movespeed * Time.deltaTime;
            yield return null;
        }
        anim.SetBool(HashCode.walkID, false);
        isMove = false;
    }
    private IEnumerator WolfWalk()
    {
        isMove = true;
        anim.SetBool(HashCode.walkID, true);
        while (!isTarget)
        {
            rb.position += Vector2.left * movespeed * Time.deltaTime;

            yield return null;
        }
        anim.SetBool(HashCode.walkID, false);
        isMove = false;
    }
    public void SetTarget(GameObject obj)
    {
        atkTarget = obj;
    }
    private IEnumerator AttackCheck()
    {
        while (!isDead)
        {           
            if (atkTarget != null && Mathf.Abs(atkTarget.transform.position.x - transform.position.x) <= range)
            {
                isTarget = true;
                if (isMove)
                {
                    StopCoroutine(walk);
                    anim.SetBool(HashCode.walkID, false);
                }
                Attack();
                yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length * 2.0f + atk_cool);
            }
            else
            {
                isTarget = false;
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }
    protected virtual void Attack()
    {
        anim.SetTrigger(HashCode.AttackID);

    }
    public virtual void HpChanged(float damage)
    {
        if(!isDead)
        {
            Hp -= damage;
        }
    }
    protected virtual void Dead()
    {
        if (gameObject.layer == 9)
        {
            bm.targetsheep = null;
        }
        else
        {
            bm.targetwolf = null;
        }
        rb.bodyType = RigidbodyType2D.Static;
        GetComponent<BoxCollider2D>().enabled = false;
        anim.SetTrigger(HashCode.DeadID);
        StartCoroutine(DeadTemp());
    }
    protected IEnumerator DeadTemp()
    {
        isDead = true;
        yield return new WaitForSeconds(2.0f);
        bm.RemoveUnit(GetComponent<Unit>());
        yield return null;
    }
    private void Update()
    {
        if (gameObject.layer == 9)
            Debug.DrawRay(transform.position, transform.TransformDirection(1.0f, 0.0f, 0.0f) * range, Color.green);
        else
            Debug.DrawRay(transform.position, transform.TransformDirection(-1.0f, 0.5f, 0.0f) * range, Color.red);
    }
}
