using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private const int ray_distance = 100, wolf_raylayer = 1 << 8, sheep_raylayer = 1 << 9, structure = 1 << 10;
    public bool isDay = true;
    public float Daytime;

    public GameObject farm, Forest;
    public List<Unit> sheeps = new List<Unit>();
    public List<Unit> wolfs = new List<Unit>();

    private int currentWave = 1, curCount = 0, deadCount = 0;
    private Stack<GameObject> walls = new Stack<GameObject>();
    private Vector2 wolf_way, sheep_way;
    private RaycastHit2D farm_ray, forest_ray;
    public GameObject targetwolf, targetsheep;

    public GameObject[] wolfUnit;

    /// <summary>
    /// 테스트용 오브젝트
    /// 패배, 승리 조건 만들어야함
    /// </summary>
    public GameObject wall, spawnSheep;
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
        if (isSheep)
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
            forest_ray = Physics2D.Raycast(Forest.transform.position, wolf_way, ray_distance, sheep_raylayer | structure);

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
    public void AddUnit(Unit unit)
    {
        if (unit.kind == 0)
        {
            unit.GetPosition(GetWall());
            sheeps.Add(unit);
            ReTargetSheep();
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

    public void RemoveUnit(Unit unit)
    {
        if (unit.kind == Unit.Kind.Sheep)
        {
            sheeps.Remove(unit);
            Destroy(unit.gameObject);
            ReTargetWolf();
            for (int i = 0; i < wolfs.Count; i++)
            {
                StartCoroutine(wolfs[i].StartOn());
            }
        }
        else
        {
            ReTargetSheep();
            wolfs.Remove(unit);
            Destroy(unit.gameObject);
            deadCount++;
            if (curCount == deadCount)
            {
                currentWave++;
                deadCount = 0;
                isDay = true;
                StartCoroutine(Timer());
            }
        }
    }
    #endregion

    #region WallSetting <Stack>

    public void SetWall(GameObject wall)
    {
        walls.Push(wall);

        for (int i = 0; i < sheeps.Count; i++)
        {
            if (!sheeps[i].isMove)
            {
                sheeps[i].GetPosition(GetWall());
                sheeps[i].Move();
            }
            else
            {
                sheeps[i].GetPosition(GetWall());
                sheeps[i].Move();
            }
        }
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

    //public void UpdateWall()
    //{
    //    for (int i = 0; i < sheeps.Count; i++)
    //    {
    //        if (!sheeps[i].isMove)
    //        {
    //            sheeps[i].GetPosition(GetWall());
    //            sheeps[i].Move();
    //        }
    //        else
    //        {
    //            sheeps[i].GetPosition(GetWall());
    //            sheeps[i].Move();
    //        }
    //    }
    //}

    #endregion

    private void WaveSetting()
    {
        DBManager.instance.LoadNextWave(currentWave);
        int i = 0;
        curCount = 0;
        while (i < DBManager.instance.waveDatas[currentWave - 1].num.Length)
        {
            for (int j = 0; j < DBManager.instance.waveDatas[currentWave - 1].num[i]; j++)
            {
                GameObject unit = Instantiate(wolfUnit[i]);
                unit.transform.position = Forest.transform.position;
                unit.GetComponent<Unit>().SetUnitData(DBManager.instance.WolfDatas[i]);
                AddUnit(unit.GetComponent<Unit>());
                curCount += 1;
            }
            i++;
        }
    }

    private IEnumerator Wave()
    {
        ReTargetWolf();
        for (int i = 0; i < wolfs.Count; i++)
        {
            StartCoroutine(wolfs[i].StartOn());
            yield return new WaitForSeconds(1.5f);
        }
        ReTargetSheep();
    }

    private IEnumerator Timer()
    {
        WaveSetting();
        yield return new WaitForSeconds(Daytime);
        isDay = false;
        StartCoroutine(Wave());
        yield return null;
    }

    private void ReTargetSheep()
    {
        targetwolf = SetAttack(true);
        for (int i = 0; i < sheeps.Count; i++)
        {
            sheeps[i].SetTarget(targetwolf);
        }
    }
    public void ReTargetWolf()
    {
        targetsheep = SetAttack(false);
        for (int i = 0; i < wolfs.Count; i++)
        {
            wolfs[i].SetTarget(targetsheep);
        }
    }
    //test용
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject a = Instantiate(sheepUnit[0], spawnSheep.transform.position, Quaternion.identity);
            a.GetComponent<Unit>().SetUnitData(DBManager.instance.SheepDatas[1]);
            StartCoroutine(a.GetComponent<Unit>().StartOn());
            AddUnit(a.GetComponent<Unit>());
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject b = Instantiate(sheepUnit[1], spawnSheep.transform.position, Quaternion.identity);
            b.GetComponent<Unit>().SetUnitData(DBManager.instance.SheepDatas[2]);
            StartCoroutine(b.GetComponent<Unit>().StartOn());
            AddUnit(b.GetComponent<Unit>());
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameObject c = Instantiate(sheepUnit[2], spawnSheep.transform.position, Quaternion.identity);
            c.GetComponent<Unit>().SetUnitData(DBManager.instance.SheepDatas[3]);
            StartCoroutine(c.GetComponent<Unit>().StartOn());
            AddUnit(c.GetComponent<Unit>());
        }
    }
}