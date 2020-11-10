using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStructure : Structure
{
    public override bool BuildCheck()
    {
        if(HP > 0 ){
            BuildingConstruct();
            return true;
        }else{
            BuildingDestroy();
            return false;
        }
    }

    private void Awake() {
        buildingSpace = 4;
        HP = 100;
    }

    
}
