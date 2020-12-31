using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeProjectile : MonoBehaviour
{
    private float atk = 0;
    private Color aColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    private int targetLayer;
    public GameObject effect;

    public void SetAtk(float _atk)
    {
        atk = _atk;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            if (gameObject.layer == 12)
                targetLayer = 1 << 9;
            else
                targetLayer = 1 << 8;

            Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, transform.localScale * 2.5f, 0, targetLayer);
            if (targets != null)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    targets[i].gameObject.GetComponent<Unit>().HpChanged(atk);
                    Debug.Log(targets[i].gameObject.name + " attack :" + atk);
                }
            }
        }
        else if(other.gameObject.layer == 10)
        {
            targetLayer = 1 << 10;
            Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, transform.localScale * 2.5f, 0, targetLayer);
            if (targets != null)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    Debug.Log(targets[i].gameObject.name + " attack :" + atk);
                    targets[i].gameObject.GetComponent<Structure>().HpChange(atk);
                }            
            }
        }
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
        GetComponent<SpriteRenderer>().color = aColor;
        effect.SetActive(true);
        Destroy(gameObject, effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
}
