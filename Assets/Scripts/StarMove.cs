using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarMove : MonoBehaviour
{
    public bool isTimeRunning = false;
    public static float TimeVal = 1000f;
    StarDataLoader instanceval;
    ConstellationCreator constinstance;
    // Start is called before the first frame update
    void Start()
    {
        instanceval = FindObjectOfType<StarDataLoader>();
        constinstance = FindObjectOfType<ConstellationCreator>();
    }
    public void ToggleTime()
    {
        isTimeRunning = !isTimeRunning;
    }
    void UpdateStarPosition(float timeDelta)
    {
        foreach (KeyValuePair<float, GameObject> starRet in instanceval.starobjects)
        {
            Vector3 displacement = instanceval.starData[starRet.Key].velocity * timeDelta;
            starRet.Value.transform.position += displacement;
        }
    }

    void Update()
    {
        if (isTimeRunning)
        {
            UpdateStarPosition(Time.deltaTime * TimeVal);
            constinstance.ConstellationSelecter(constinstance.selectedConst);
        }
    }


    // Update is called once per frame
   
}
