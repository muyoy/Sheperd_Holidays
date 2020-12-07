using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private const int ray_distance = 100, wolf_layer = 8, sheep_layer = 9, wolf_raylayer = 1 << 8, sheep_raylayer = 1 << 9;
    public bool isDay = true, isClear = true;
    public float Daytime;

    public DBManager db;
    public GameObject farm, Forest;
    public List<GameObject> sheeps = new List<GameObject>();
    public List<GameObject> wolfs = new List<GameObject>();

    private int currentWave = 1;
    private Stack<GameObject> walls = new Stack<GameObject>();
    private Vector2 wolf_way, sheep_way;
    private RaycastHit2D farm_ray, forest_ray;
    private GameObject targetwolf, targetsheep;

    public GameObject[] wolfUnit;

    /// <summary>
    /// 테스트용 오브젝트
    /// </summary>
    public GameObject wall, tile, spawnSheep, spawnWolf;
    public GameObject[] sheepUnit;

    private void Awake()
    {
        SetWall(wall);
    }
    private void Start()
    {
        wolf_way = Vector2.left;
        sheep_way = Vector2.right;

        StartCoroutine(Timer());
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

    private IEnumerator WaveSetting()
    {
        isClear = false;
        db.LoadNextWave(currentWave);
        int i = 0;
        while (i < db.waveDatas[currentWave - 1].num.Length)
        {
            for (int j = 0; j < db.waveDatas[currentWave - 1].num[i]; j++)
            {
                GameObject unit = Instantiate(wolfUnit[i]);
                unit.transform.position = Forest.transform.position;
                AddUnit(unit);
            }
            i++;
            yield return null;
        }
    }

    private IEnumerator Wave()
    {
        for (int i = 0; i < wolfs.Count; i++)
        {
            StartCoroutine(wolfs[i].GetComponent<Unit>().StartOn());
            yield return new WaitForSeconds(1.5f);
        }

        yield return new WaitForSeconds(2.0f);

        ReTarget();

        targetsheep = SetAttack(false);
        for (int k = 0; k < wolfs.Count; k++)
        {
            wolfs[k].GetComponent<Unit>().SetTarget(targetsheep);
        }
    }

    private void Battle()
    {
        while(!isClear && !isDay)
        {
            for(int i =0; i< wolfs.Count; i++)
            {
                if(wolfs[i].GetComponent<Unit>().isDead == false)
                {
                    Battle();
                }
            }

            isClear = true;
            isDay = true;
        }

    }

    private IEnumerator Timer()
    {
        if (isDay)
        {
            yield return new WaitForSeconds(Daytime);
            isDay = false;
            StartCoroutine(Wave());
        }
        yield return null;
    }

    private void ReTarget()
    {
        targetwolf = SetAttack(true);
        for (int k = 0; k < sheeps.Count; k++)
        {
            sheeps[k].GetComponent<Unit>().SetTarget(targetwolf);
        }
    }
    public void ReTargetWolf()
    {
        targetsheep = SetAttack(false);
        for (int k = 0; k < wolfs.Count; k++)
        {
            wolfs[k].GetComponent<Unit>().SetTarget(targetsheep);
        }
    }
    //test용
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject a = Instantiate(sheepUnit[0], spawnSheep.transform.position, Quaternion.identity);
            StartCoroutine(a.GetComponent<Unit>().StartOn());
            AddUnit(a);
            ReTarget();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject b = Instantiate(sheepUnit[1], spawnSheep.transform.position, Quaternion.identity);
            StartCoroutine(b.GetComponent<Unit>().StartOn());
            AddUnit(b);
            ReTarget();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameObject c = Instantiate(sheepUnit[2], spawnSheep.transform.position, Quaternion.identity);
            StartCoroutine(c.GetComponent<Unit>().StartOn());
            AddUnit(c);
            ReTarget();
        }

        if(isClear)
        {
            isDay = true;
            StartCoroutine(WaveSetting());
        }
    }
}