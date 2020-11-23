using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Long_Unit : Unit
{
    public GameObject projectile;
    private const float max_hp = 60.0f;
    //private Coroutine walk = null;
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
        hp = max_hp;
        atk = 30;
        range = (float)type * gridSize;
        atk_cool = 2.0f;
        isMove = true;
    }
    #region Long_UnitMove

    //protected override void Move()
    //{
    //    //if (kind == Kind.Sheep)
    //    //{
    //    //    targetPos += new Vector3((int)type * gridSize, 0.0f, 0.0f);
    //    //    tab = Random.Range(-0.3f, 0.3f);
    //    //    targetPos += new Vector3(tab, 0.0f, 0.0f);
    //    //    if (isMove)
    //    //    {
    //    //        walk = StartCoroutine(SheepWalk(targetPos));
    //    //    }
    //    //    else
    //    //    {
    //    //        StopCoroutine(walk);
    //    //        walk = StartCoroutine(SheepWalk(targetPos));
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    StartCoroutine(WolfWalk());
    //    //}
    //}

    //private IEnumerator SheepWalk(Vector3 _targetPos)
    //{
    //    isMove = false;
    //    anim.SetBool(HashCode.walkID, true);
    //    while ((_targetPos.x - transform.position.x) >= 0)
    //    {
    //        rb.position += Vector2.right * speed * Time.deltaTime;
    //        yield return null;
    //    }
    //    anim.SetBool(HashCode.walkID, false);
    //    isMove = true;
    //}
    //private IEnumerator WolfWalk()
    //{
    //    anim.SetBool(HashCode.walkID, true);
    //    while (!isTarget)
    //    {
    //        rb.position += Vector2.left * speed * Time.deltaTime;

    //        yield return null;
    //    }
    //    anim.SetBool(HashCode.walkID, false);
    //}

    #endregion

    protected override void Attack()
    {
        
    }
}
