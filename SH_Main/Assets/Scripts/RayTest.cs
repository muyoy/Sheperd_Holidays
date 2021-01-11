using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    private const int ray_distance = 7, wolf_raylayer = 1 << 8;
    private Vector2 sheep_way;
    private Animator anim;
    private RaycastHit2D farm_ray;
    void Awake()
    {
        sheep_way = Vector2.right;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        farm_ray = Physics2D.Raycast(transform.position, sheep_way, ray_distance, wolf_raylayer);

#if UNITY_EDITOR
        Debug.DrawRay(transform.position, sheep_way * ray_distance, Color.green);
#endif
        if (farm_ray.transform != null)
        {
            anim.SetTrigger(HashCode.AttackID);
        }
    }
}