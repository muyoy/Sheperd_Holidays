using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    protected int hitPoints;
    public int HP{
        get { return hitPoints; }
        set {
            hitPoints = value;
        }
    }

    public virtual bool buildCheck(){ return true; }  // 건물이 지어졌는지 체크

    protected int cost;
    protected float buildTime; // 건물이 지어질 때 필요한 시간
    protected int buildingSpace; // 건물이 지어질 때 필요한 공간

    protected virtual void buildingFunc(){}
    protected virtual float HpChange(float damage){ return 0f; }

}
