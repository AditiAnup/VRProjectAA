using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StarData
{
    public float absMag;
    public float relativeMag;
    public Vector3 position;
    public float hip;
    public string color;
    public float id;

    public float size;
    
    public float GetSize(float relatievMag)
    {
        return 1 - Mathf.InverseLerp(-146, 796, relatievMag);
    }

}
