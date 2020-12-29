using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill : Structure
{
    [Space(1.0f)]
    [Header("Windmill Parameter")]
    public int Level = 0;
    public enum State { Build, Product, Destroy };
    public State state;
    public int resource = 0;
    public float resourceCreateTime = 5.0f;
    public bool isProduction = false;

    public GameObject[] BuildingLevel;

    private Coroutine resourceCreate = null;

    public override void Init()
    {
        base.Init();
        StartCoroutine(BuildStructure());
    }

    IEnumerator BuildStructure()
    {
        state = State.Build;
        StopResourceCreate();
        float time = 0.0f;
#if UNITY_EDITOR
        Debug.Log("건물 지을때의 이펙트 필요!!");
#endif
        while (time <= buildTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
#if UNITY_EDITOR
        Debug.Log("건물 공사 완료시 이펙트 필요!!");
#endif
        yield return null;
        Level++;
        ChangeBuildingImage();
        resourceCreate = StartCoroutine(ResourceCreate());
    }

    IEnumerator ResourceCreate()
    {
        state = State.Product;
        isProduction = true;
        float time = 0.0f;
        while (isProduction)
        {
            time += Time.deltaTime;
            if (resourceCreateTime <= time)
            {
                resource = CreateResource();
                time = 0.0f;
            }
            yield return null;
        }

        yield return null;
    }

    public void BuildButton()
    {
        if (state == State.Build) return;

        StartCoroutine(BuildStructure());
    }

    public void ResourceGathering()
    {
        //TODO : DB += resource * factor
        resource = 0;
    }

    private int CreateResource()
    {
        switch(Level)
        {
            case 1: return 1;
            case 2: return 2;
            case 3: return 4;
            default: return 0;
        }
    }

    private void ChangeBuildingImage()
    {
        switch (Level)
        {
            case 1:
                BuildingLevel[0].SetActive(true);
                break;
            case 2:
                BuildingLevel[0].SetActive(false);
                BuildingLevel[1].SetActive(true);
                break;
            case 3:
                BuildingLevel[1].SetActive(false);
                BuildingLevel[2].SetActive(true);
                break;
            default:
                break;
        }
    }

    private void StopResourceCreate()
    {
        if (resourceCreate != null)
        {
            StopCoroutine(resourceCreate);
            resourceCreate = null;
        }
    }

    protected override int HpChange(float damage)
    {
        return base.HpChange(damage);
    }

}
