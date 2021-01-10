using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public enum Kind { Sheep, Wolf }
    public enum Type { Short, Middle, Long }
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
    public bool isHit;
    [SerializeField] protected Image HpBar;
    protected Rigidbody2D rb;
    protected Animator anim;
    private Coroutine walk = null;
    private Coroutine attack = null;
    private Coroutine revenge = null;

    public GameObject[] dmgTMP;
    private int count = 0;
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


        if (attack != null)
        {
            StopCoroutine(attack);
            attack = StartCoroutine(AttackCheck());
        }
        else
            attack = StartCoroutine(AttackCheck());

        Move();
    }
    protected virtual void Init() { }

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
    public void SetUnitData(DBStruct.WolfData wolfData)
    {
        maxHp = wolfData.hp;
        hp = maxHp;
        float atkRange = wolfData.atkRange;
        range = atkRange * gridSize;
        atk = wolfData.atk;
        atk_cool = wolfData.atkDelay;
        movespeed = wolfData.moveSpeed;
        Atktype = (AtkType)wolfData.type;
    }
    public virtual void Move()
    {
        if (kind == Kind.Sheep)
        {
            if (type == Type.Short)
                targetPos = bm.rallyPoint[2];
            else if (type == Type.Middle)
                targetPos = bm.rallyPoint[1];
            else
                targetPos = bm.rallyPoint[0];

            tab = Random.Range(-0.6f, 0.0f);
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
            if (!isMove)
            {
                walk = StartCoroutine(WolfWalk());
            }
            else
            {
                StopCoroutine(walk);
                anim.SetBool(HashCode.walkID, false);
                walk = StartCoroutine(WolfWalk());
            }
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
            if (transform.position.x > bm.GetWall().transform.position.x)
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
        yield return new WaitForSeconds(1.0f);
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
                    isMove = false;
                    anim.SetBool(HashCode.walkID, false);
                }
                if (isHit)
                {
                    StopCoroutine(revenge);
                    isHit = false;
                    anim.SetBool(HashCode.walkID, false);
                }
                Attack();
                yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + atk_cool);
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
    private IEnumerator HitAttack()
    {
        while(Mathf.Abs(atkTarget.transform.position.x - transform.position.x) >= range)
        {
            anim.SetBool(HashCode.walkID, true);
            rb.position += Vector2.right * movespeed * Time.deltaTime;
            //TODO : 버그 수정하기
            //if(Mathf.Abs(bm.GetWall().transform.position.x - transform.position.x) > (1.28f * 5.0f)+0.64f)
            //{
            //    transform.localScale = Vector3.one;
            //    float temp = movespeed;
            //    movespeed = 2.0f;
            //    rb.position += Vector2.left * movespeed * Time.deltaTime;
            //    if(targetPos.x - transform.position.x >= 0.0f)
            //    {
            //        transform.localScale = new Vector3(-1.0f, 0.0f, 0.0f);
            //        movespeed = temp;
            //        anim.SetBool(HashCode.walkID, false);
            //    }
            //}
            //else
            //{
            //    rb.position += Vector2.right * movespeed * Time.deltaTime;
            //}
            yield return null;
        }
        yield return new WaitForSeconds(5.0f);
    }
    public virtual void HpChanged(float damage)
    {
        if (!isDead)
        {
            Hp -= damage;
            HpBar.fillAmount = Hp / maxHp;
            if (kind == Kind.Sheep)
            {
                isHit = true;
                if (revenge != null)
                {
                    StopCoroutine(revenge);
                    revenge = StartCoroutine(HitAttack());
                }
                else
                {
                    revenge = StartCoroutine(HitAttack());
                }
            }
        }
        StartCoroutine(dmgTMP[count].GetComponent<DmgEffect>().Dmg(damage));
        ++count;
        if (count >= dmgTMP.Length)
            count = 0;
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
