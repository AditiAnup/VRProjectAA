using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartTelport : MonoBehaviour
{
    public GameObject startPos;
    public GameObject playercont;
    Dictionary<float, GameObject> resObjects = new Dictionary<float, GameObject>();
    Dictionary<float, StarData> starReset = new Dictionary<float, StarData>();
    StarDataLoader instanceval;
    StarMove moveinstance;
    void Start()
    {
        instanceval =  FindObjectOfType<StarDataLoader>();
        moveinstance = FindObjectOfType<StarMove>();
        resObjects = instanceval.starobjects;
        starReset = instanceval.starData;
    }
    public void StartPos()
    {
        playercont.transform.position = startPos.transform.position;
        playercont.transform.rotation = startPos.transform.rotation;
        moveinstance.isTimeRunning = false;
        foreach (KeyValuePair<float, GameObject> starRet in instanceval.starobjects)
        {
            starRet.Value.transform.position = starReset[starRet.Key].position;
        }
    }
}
