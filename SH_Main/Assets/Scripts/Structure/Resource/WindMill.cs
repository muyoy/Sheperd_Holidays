using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMill : Structure
{
    public GameObject windmillWing;
    public override void Init()
    {
        base.Init();
        windmillWing.SetActive(true);
    }

    private void Update()
    {
        float angle = 0;
        
        windmillWing.transform.Rotate(new Vector3(0, 0, angle));
        angle += 1f;
    }
}
