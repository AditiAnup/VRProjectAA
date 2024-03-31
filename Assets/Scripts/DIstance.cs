using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class DIstance : MonoBehaviour
{
    //public TextMesh text;
    public GameObject text;
    public GameObject Sol;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Sol.transform.LookAt(Vector3.zero);
        text.transform.rotation = Camera.main.transform.rotation;
        text.GetComponent<TextMeshPro>().text = "Distance from sun: " + Vector3.Magnitude(gameObject.transform.position) + " parsecs";
    }
}
