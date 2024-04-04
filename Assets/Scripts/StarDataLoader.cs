using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;


public class StarDataLoader : MonoBehaviour
{
    public TextAsset dSource;
    public GameObject cam;
    public GameObject starPrefab;
    private int starCount = 1;

    public static float TimeVal = 1000f;
    public bool isTimeRunning = false;


    ExoColor colorinst;
    public Dictionary<float, GameObject> starobjects = new Dictionary<float, GameObject>();
    public Dictionary<float, StarData> starData = new Dictionary<float, StarData>();

    void Start()
    {
        colorinst = FindObjectOfType<ExoColor>();
        ParseFile();

    }



    void Update()
    {
        StarRendering();

    }

    float ConvertVelocity(float velocity)
    {
        const float convFactor = 1.02269e-6f;
        return velocity * convFactor;
    }

    void ParseFile()
    {
        string[] lines = dSource.text.Split('\n');
        bool firstLineRead = false;
        foreach (string line in lines)
        {
            if (!firstLineRead)
            {
                firstLineRead = true;
                continue;
            }

            try
            {


                string[] csvVal = line.Split(new[] { "," }, StringSplitOptions.None);
                float x = float.TryParse(csvVal[3], out float xResult) ? xResult : 0.0f;
                float y = float.TryParse(csvVal[4], out float yResult) ? yResult : 0.0f;
                float z = float.TryParse(csvVal[5], out float zResult) ? zResult : 0.0f;

                float Vx = ConvertVelocity(float.TryParse(csvVal[8], out float vxResult) ? vxResult : 0.0f);
                float Vy = ConvertVelocity(float.TryParse(csvVal[9], out float vyResult) ? vyResult : 0.0f);
                float Vz = ConvertVelocity(float.TryParse(csvVal[10], out float vzResult) ? vzResult : 0.0f);

                Vector3 Parsecvelocity = new Vector3(Vx, Vy, Vz);

                float.TryParse(csvVal[6], out float scale);
                float.TryParse(csvVal[0], out float id);
                float.TryParse(csvVal[1], out float hip);
                StarData star = new StarData();
                star.size = star.GetSize(scale);
                star.position = new Vector3(x, y, z);
                star.velocity = Parsecvelocity;
                float.TryParse(csvVal[5], out float absoluteMagnituge);
                star.absMag = absoluteMagnituge;
                star.id = id;
                star.hip = hip;
                star.color = csvVal[11].Trim().ToString();

                starData.Add(hip, star);
                GameObject stars = Instantiate(starPrefab, star.position, Quaternion.LookRotation(star.position), gameObject.transform);
                stars.name = "S" + starCount;
                stars.transform.localScale = new Vector3(star.size, star.size, star.size);
                starCount++;
                starobjects[hip] = stars;
                

            }
            catch (IndexOutOfRangeException)
            {
                continue;
            }

        }
        colorinst.originalColor();
    }
    void StarRendering()
    {
        foreach (KeyValuePair<float, GameObject> starRet in starobjects)
        {
            bool isCloseEnough = Vector3.Distance(cam.transform.position, starRet.Value.transform.position) <= 50f;

            Renderer starRenderer = starRet.Value.GetComponent<Renderer>();
            if (starRenderer != null)
            {
                starRenderer.enabled = isCloseEnough;
            }
        }
    }




}
