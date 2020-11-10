using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private const int ray_distance = 7000, wolf_layer = 1 << 8, sheep_layer = 1 << 9;

    public GameObject farm, Forest;
    public List<GameObject> sheeps = new List<GameObject>();
    public List<GameObject> wolfs = new List<GameObject>();

    private Stack<GameObject> walls = new Stack<GameObject>();
    private Vector2 wolf_way, sheep_way;
    private RaycastHit2D farm_ray, forest_ray;


    /// <summary>
    /// 테스트용 오브젝트
    /// </summary>
    public GameObject wolf, sheep, wall;

    private void Awake()
    {
        SetWall(wall);
    }

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

#if UNITY_EDITOR
            Debug.DrawRay(farm.transform.position, sheep_way * ray_distance, Color.green);
#endif
            if (farm_ray.transform == null)
            {
                return null;
            }
            return farm_ray.collider.gameObject;
        }
        else
        {
            forest_ray = Physics2D.Raycast(Forest.transform.position, wolf_way, ray_distance, sheep_layer);

#if UNITY_EDITOR
            Debug.DrawRay(Forest.transform.position, wolf_way * ray_distance, Color.red);           
#endif
            if (forest_ray.transform == null)
            {
                return null;
            }
            return forest_ray.collider.gameObject;
        }
    }

    #region UnitSetting <List>
    public void AddUnit(GameObject unit)
    {
        if (unit.layer == sheep_layer)
            sheeps.Add(unit);
        else
            wolfs.Add(unit);
    }

    public int GetCount()
    {
        return sheeps.Count;
    }

    public void RemoveUnit(GameObject unit)
    {
        if (unit.layer == sheep_layer)
            sheeps.Remove(unit);
        else
            wolfs.Remove(unit);
    } 
    #endregion

    #region WallSetting <Stack>

    public void SetWall(GameObject wall)
    {
        walls.Push(wall);
    }
    public GameObject GetWall()
    {
        if (walls.Count != 0)
            return walls.Peek();
        else
            return null;
    }
    public void RemoveWall(GameObject wall)
    {
        walls.Pop();
    }

    #endregion
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