using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeElapsed : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject text;
    StarMove moveinst;
    void Start()
    {
        text.SetActive(false);
        moveinst = FindObjectOfType<StarMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void timeval()
    {
        
    }
}
