using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float atk = 0;

    public void SetAtk(float _atk)
    {
        atk = _atk;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
        {
            other.gameObject.GetComponent<Unit>().HpChanged(atk);
            Debug.Log(atk);
            Destroy(gameObject);
        }
    }
}
