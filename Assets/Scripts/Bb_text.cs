using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bb_text : MonoBehaviour
{
    public float farDist = 3f;
    public float nearDist = 0.5f;
    private MeshRenderer mR;
    public string data;
    public string exo_data;
    public GameObject stars;
    StarDataLoader loadStars;
    private bool show_exo = false;
    void Start()
    {
        stars = gameObject.transform.parent.transform.parent.gameObject;
        mR = gameObject.GetComponent<MeshRenderer>();
        loadStars = stars.GetComponent<StarDataLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        var distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        if(distance > farDist)
        {
            mR.enabled = false;
        }
        else if (distance < nearDist)
        {
            mR.enabled = false;
        }
        else
        {
            mR.enabled = true;
        }
    }
}
