﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StarMove : MonoBehaviour
{
    //Changed File
    public float elapsedtime = 0;
    public bool isTimeRunning = false;
    public float TimeVal = 1000f;
    StarDataLoader instanceval;
    ConstellationCreator constinstance;
    public GameObject text;
    void Start()
    {
        instanceval = FindObjectOfType<StarDataLoader>();
        constinstance = FindObjectOfType<ConstellationCreator>();
        InvokeRepeating("UpdateStarPosition", 0f, 0.5f);
        
    }
    public void ToggleTime(bool value)
    {
        isTimeRunning = value;
        if(!isTimeRunning)
        {
            elapsedtime = 0;
            text.SetActive(false);
        }
        else
        {
            text.SetActive(true);
        }
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
            constinstance.ConstellationSelecter(constinstance.selectedConst);


        }
    }

    public void ReverseTime()
    {
        TimeVal = -TimeVal;
    }
    private void Update()
    {
        
        
        if (isTimeRunning)
        {
            //foreach (KeyValuePair<float, GameObject> starRet in instanceval.starobjects)
            //{
            //    Vector3 displacement = instanceval.starData[starRet.Key].velocity *TimeVal * Time.deltaTime;
            //    starRet.Value.transform.position += displacement;

            //}
            if(TimeVal<0)
            {
                elapsedtime -= Time.deltaTime;
            }
            else
            {
                elapsedtime += Time.deltaTime;
            }

            
            text.GetComponent<TextMeshPro>().text = "Time Elapsed: " + elapsedtime.ToString("F2") + " years";

        }
    }


}
