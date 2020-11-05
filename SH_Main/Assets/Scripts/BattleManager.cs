using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private const int ray_distance = 7000, wolf_layer = 1 << 8, sheep_layer = 1 << 9;
    public GameObject farm, Forest;
    public List<GameObject> sheeps = new List<GameObject>();
    public List<GameObject> wolfs = new List<GameObject>();

    private Vector2 wolf_way, sheep_way;
    private RaycastHit2D farm_ray, forest_ray;

    //test용
    public GameObject wolf, sheep;
    //
    private void Start()
    {
        wolf_way = Vector2.left;
        sheep_way = Vector2.right;
    }

    public GameObject SetAttack(bool isSheep)
    {
        if(isSheep)
        {
            farm_ray = Physics2D.Raycast(farm.transform.position, sheep_way, ray_distance, wolf_layer);
            Debug.DrawRay(farm.transform.position, sheep_way * ray_distance, Color.green);
            if (farm_ray.transform == null)
            {
                return null;
            }
            return farm_ray.collider.gameObject;
        }
        else
        {
            forest_ray = Physics2D.Raycast(Forest.transform.position, wolf_way, ray_distance, sheep_layer);
            Debug.DrawRay(Forest.transform.position, wolf_way * ray_distance, Color.red);
            if (forest_ray.transform == null)
            {
                return null;
            }
            return forest_ray.collider.gameObject;
        }
    }
    //test용
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.A))
        {
            wolf = SetAttack(true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            sheep = SetAttack(false);
        }
    }
}
/*
RaycastHit2D hit;
hit = Physics2D.Raycast(transform.position, town_vec, 5.0f, LayerMask.GetMask("Water"));
Debug.DrawRay(transform.position, town_vec* 5.0f, Color.green);
*/