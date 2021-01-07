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

    public BuildingEffect effect;

    public override void Init()
    {
        base.Init();

        effect = transform.Find("BuildingEffect").GetComponent<BuildingEffect>();
        StartCoroutine(BuildStructure());
        Invoke("BuildButton", 5.0f);
        Invoke("BuildButton", 10.0f);
    }

    IEnumerator BuildStructure()
    {
        state = State.Build;
        StopResourceCreate();

        if (Level >= 1)
        {
            BuildingLevel[Level - 1].SetActive(false);
        }
        effect.gameObject.SetActive(true);
        effect.PlayBuildingEffect(buildTime);

        while (!effect.isBuildProgressEnd) { yield return null; }

        Level++;
        yield return null;
        ChangeBuildingImage();
        yield return null;
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
        switch (Level)
        {
            case 1: return 1;
            case 2: return 2;
            case 3: return 4;
            default: return 0;
        }
    }

    private void ChangeBuildingImage()
    {
        //BuildingLevel[Level - 2].SetActive(false);
        BuildingLevel[Level - 1].SetActive(true);
    }

    private void StopResourceCreate()
    {
        if (resourceCreate != null)
        {
            StopCoroutine(resourceCreate);
            resourceCreate = null;
        }
    }

    public override int HpChange(float damage)
    {
        return base.HpChange(damage);
    }

    protected override void BuildingFunc()
    {
        base.BuildingFunc();
    }
}
