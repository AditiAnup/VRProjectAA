using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartTelport : MonoBehaviour
{
    public GameObject startPos;
    public GameObject playercont;
    public void StartPos()
    {
        playercont.transform.position = startPos.transform.position;
        playercont.transform.rotation = startPos.transform.rotation;
    }
}
