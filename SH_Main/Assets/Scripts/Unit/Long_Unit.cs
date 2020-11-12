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
        range = (float)type * gridSize * 3;
        atk_cool = 2.0f;
        Move();
    }

    protected override void Move()
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
        while ((_targetPos.x - transform.position.x) >= 0)
        {
            rb.position += Vector2.right * speed * Time.deltaTime;
            yield return null;
        }
        anim.SetBool(HashCode.walkID, false);
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
    protected override void Attack()
    {
        
    }

}
