using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarMove : MonoBehaviour
{
    public bool isTimeRunning = false;
    public static float TimeVal = 20000f;
    StarDataLoader instanceval;
    ConstellationCreator constinstance;
    void Start()
    {
        instanceval = FindObjectOfType<StarDataLoader>();
        InvokeRepeating("UpdateStarPosition", 0f, 0.5f);
    }
    public void ToggleTime(bool value)
    {
        isTimeRunning = value;
    }
    void UpdateStarPosition()
    {
        
        if (isTimeRunning)
        {
            Debug.Log("Update Star Position");
            foreach (KeyValuePair<float, GameObject> starRet in instanceval.starobjects)
            {
                Vector3 displacement = instanceval.starData[starRet.Key].velocity * TimeVal * Time.deltaTime;
                starRet.Value.transform.position += displacement;
            }
        }
    }

    
}
