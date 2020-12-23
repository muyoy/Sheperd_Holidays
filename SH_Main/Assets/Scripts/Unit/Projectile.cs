using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float atk = 0;
    public GameObject effect;
    private Color aColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    public void SetAtk(float _atk)
    {
        atk = _atk;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            other.gameObject.GetComponent<Unit>().HpChanged(atk);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
            GetComponent<SpriteRenderer>().color = aColor;
            effect.SetActive(true);
            Debug.Log(atk);
            Destroy(gameObject, effect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
