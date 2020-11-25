using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private const int ray_distance = 100, wolf_layer = 8, sheep_layer = 9, wolf_raylayer = 1 << 8, sheep_raylayer = 1 << 9;

    public GameObject farm, Forest;
    public List<GameObject> sheeps = new List<GameObject>();
    public List<GameObject> wolfs = new List<GameObject>();

    private Stack<GameObject> walls = new Stack<GameObject>();
    private Vector2 wolf_way, sheep_way;
    private RaycastHit2D farm_ray, forest_ray;


    /// <summary>
    /// 테스트용 오브젝트
    /// </summary>
    public GameObject wolf, sheep, wall, tile, spawnSheep, spawnWolf;
    public GameObject[] sheepUnit;

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
            farm_ray = Physics2D.Raycast(farm.transform.position, sheep_way, ray_distance, wolf_raylayer);

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
            forest_ray = Physics2D.Raycast(Forest.transform.position, wolf_way, ray_distance, sheep_raylayer);

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
        {
            unit.GetComponent<Unit>().GetPosition(GetWall());
            sheeps.Add(unit);
        }
        else
        {
            wolfs.Add(unit);
        }
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

    public void UpdateWall()
    {
        for (int i = 0; i < sheeps.Count; i++)
        {
            if(!sheeps[i].GetComponent<Unit>().isMove)
            {
                sheeps[i].GetComponent<Unit>().GetPosition(GetWall());
                StartCoroutine(sheeps[i].GetComponent<Unit>().StartOn());
            }
            else
            {
                sheeps[i].GetComponent<Unit>().GetPosition(GetWall());
                StartCoroutine(sheeps[i].GetComponent<Unit>().StartOn());
            }
        }
    }

    #endregion



    //test용
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            wolf = SetAttack(true);
            for (int i = 0; i < sheeps.Count; i++)
            {
                sheeps[i].GetComponent<Unit>().SetTarge(wolf);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            sheep = SetAttack(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject a = Instantiate(sheepUnit[0], spawnSheep.transform.position, Quaternion.identity);
            AddUnit(a);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject b = Instantiate(sheepUnit[1], spawnSheep.transform.position, Quaternion.identity);
            AddUnit(b);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameObject c = Instantiate(sheepUnit[2], spawnSheep.transform.position, Quaternion.identity);
            AddUnit(c);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject a = Instantiate(wall);
            a.transform.SetParent(tile.transform);
            a.transform.localPosition = new Vector3(0.0f, 2.0f, 0.0f);
            SetWall(a);
            UpdateWall();
        }
    }
}