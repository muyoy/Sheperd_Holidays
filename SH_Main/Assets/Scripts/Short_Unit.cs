using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Short_Unit : Unit
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
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        hp = max_hp;
        atk = 30;
        range = 20;
        atk_cool = 2.0f;
        Move();
    }

    protected override void Move()
    {
        base.Move();
        targetPos += new Vector2((int)type, transform.position.y);
        StartCoroutine(Walk(targetPos));
    }

    private IEnumerator Walk(Vector2 _targetPos)
    {
        ani.SetBool("Walk", true);
        Debug.Log(_targetPos);
        while (Vector2.Distance(transform.position, _targetPos) > 0)
        {
            rb.position += Vector2.right * speed * Time.deltaTime;
            Debug.Log(Vector2.Distance(transform.position, _targetPos));
            yield return null;
        }
        ani.SetBool("Walk", false);
    }

    protected override void Attack()
    {

    }
}
