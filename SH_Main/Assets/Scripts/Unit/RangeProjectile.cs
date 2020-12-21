using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeProjectile : MonoBehaviour
{
    private float atk = 0;
    private int targetLayer;

    public void SetAtk(float _atk)
    {
        atk = _atk;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
            targetLayer = 1 << 8;
        else
            targetLayer = 1 << 9;

        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, transform.localScale * 2.5f , 0, targetLayer);
        if (targets != null)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].gameObject.GetComponent<Unit>().HpChanged(atk);
                Debug.Log(targets[i].gameObject.name + " attack :" + atk);
            }
            Destroy(gameObject);
        }
    }
}
