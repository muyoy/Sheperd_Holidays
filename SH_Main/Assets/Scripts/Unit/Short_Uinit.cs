using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Short_Uinit : Unit
{
    private const float max_hp = 60.0f;
    protected override void Awake()
    {
        base.Awake();
        type = Type.Short;

        if (gameObject.layer == 9)
            kind = Kind.Sheep;
        else
            kind = Kind.Wolf;
    }
    protected override void Start()
    {
        Init();
        base.Start();
    }

    protected override void Init()
    {
        hp = max_hp;
        atk = 30;
        range = (float)type * gridSize;
        atk_cool = 2.0f;
    }

    #region Short_UnitMove
    public override void Move()
    {
        if (kind == Kind.Sheep)
        {
            base.Move();
            targetPos += new Vector3((int)type * gridSize, 0.0f, 0.0f);
            StartCoroutine(SheepWalk(targetPos));
        }
        else
        {
            StartCoroutine(WolfWalk());
        }
    }
    private IEnumerator SheepWalk(Vector3 _targetPos)
    {
        anim.SetBool(HashCode.walkID, true);
        while ((_targetPos.x - transform.position.x) >= 0 && isMove)
        {
            rb.position += Vector2.right * speed * Time.deltaTime;
            yield return null;
        }
        anim.SetBool(HashCode.walkID, false);
        isMove = false;
    }
    private IEnumerator WolfWalk()
    {
        anim.SetBool(HashCode.walkID, true);
        while (!isTarget)
        {
            rb.position += Vector2.left * speed * Time.deltaTime;
            yield return null;
        }
        anim.SetBool(HashCode.walkID, false);
    } 
    #endregion
    protected override void Attack()
    {

    }
}
