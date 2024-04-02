using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class StarDataLoader : MonoBehaviour
{
    public TextAsset dSource;
    public GameObject starPrefab;
    private int starCount = 1;
    public GameObject linePrefab;
    public TextAsset constellationsrc;
    public TextAsset exo_data;
    //private Dictionary<string, StarData> 
    public bool show_exo = false;
    public List<GameObject> constellations = new List<GameObject>();

    public TextAsset[] constellationFiles;
    public Color[] colorArray = new Color[] // Colors for OBAFGKM spectral types
    {
        new Color(155f / 255f, 176f / 255f, 255f / 255f, 1f), // O: Blue
        new Color(170f / 255f, 191f / 255f, 255f / 255f, 1f), // B: Blue White
        new Color(202f / 255f, 215f / 255f, 255f / 255f, 1f), // A: White
        new Color(248f / 255f, 247f / 255f, 255f / 255f, 1f), // F: Yellow White
        new Color(255f / 255f, 244f / 255f, 234f / 255f, 1f), // G: Yellow
        new Color(255f / 255f, 210f / 255f, 161f / 255f, 1f), // K: Orange
        new Color(255f / 255f, 204f / 255f, 111f / 255f, 1f)  // M: Red
    };
    private Dictionary<float, GameObject> starobjects = new Dictionary<float, GameObject>();
    public Dictionary<float, StarData> starData = new Dictionary<float, StarData>();
    

    void Start()
    {
        ParseFile();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Hello Bitches");
            show_exoData();
        }

    }

    public void HideConstellations()
    {
        if (constellations != null)
        {
            foreach (GameObject constellation in constellations)
            {
                constellation.SetActive(false);
            }
        }
    }

    public void show_exoData()
    {
        //Dictionary<string, GameObject> Exo_stars = new Dictionary<string, GameObject>();
        Debug.Log("Entered exo function");
        string allData3 = exo_data.text;
        string[] lineData3 = allData3.Split('\n');
        //for (var i = 1; i < lineData3.Length - 1; i++)
        //{
        //    string[] eachline3 = lineData3[i].Split(',');
        //    string hip_number = (eachline3[1]);

        //    //if (KeyValuePair<string, GameObject> star in starObjects && )
        //    Exo_stars.Add(eachline3[1], starObjects[eachline3[1]]);

        //}


        for (var i = 1; i < lineData3.Length - 1; i++)
        {
            string[] eachline3 = lineData3[i].Split(',');
            float.TryParse(eachline3[1], out float hip_number);
            //string hip_number = eachline3[1].Trim().ToString();
                
            string colorval = eachline3[2].Trim().ToString();
            if (colorval != null)
            {
                if (colorval == '1'.ToString())
                {
                    Debug.Log(hip_number);
                    //star.GetComponent<Renderer>().material.SetColor("_Color", color_arr[spectral_types[colorval]]);
                    //star.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(55, 102, 250, 139));
                    starobjects[hip_number].GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                    Debug.Log(colorval);
                }

                else if (colorval == '2'.ToString())
                {
                    //star.GetComponent<Renderer>().material.color = Color.cyan;
                    //star.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(110, 160, 250, 139));
                    //starList[hip_number].GetComponent<Renderer>().material.SetColor("_TintColor", Color.cyan);
                    starobjects[hip_number].GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
                    Debug.Log(colorval);
                }

                else if (colorval == '3'.ToString())
                {
                    //star.GetComponent<Renderer>().material.color = Color.white;
                    //star.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(204, 225, 250, 139));
                    starobjects[hip_number].GetComponent<Renderer>().material.SetColor("_Color", Color.grey);
                    Debug.Log(colorval);
                }

                else if (colorval == '4'.ToString())
                {
                    //star.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(55.0f, 102.0f, 250.0f, 55.0f));
                    starobjects[hip_number].GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                    Debug.Log(colorval);
                }

                else if (colorval == '5'.ToString())
                {
                    starobjects[hip_number].GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                    //star.GetComponent<Renderer>().material.SetColor("_TintColor", new Color(265, 165, 35, 103));
                    Debug.Log(colorval);
                }

                else if (colorval == '6'.ToString())
                {
                    starobjects[hip_number].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    Debug.Log(colorval);
                }


            }

            else
            {
                starobjects[hip_number].GetComponent<Renderer>().material.SetColor("_Color", Color.white);

            }
        }





    }

    public void ConstellationSelecter(int i)
    {
        CreateContellations(constellationFiles[i]);
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
    }

    private void SetStarColor(GameObject star, Color color)
    {
        Renderer renderer = star.GetComponent<Renderer>();
        if (renderer != null)
        {
            
            Material starMaterial = new Material(renderer.sharedMaterial);
            starMaterial.color = color;
            renderer.material = starMaterial;
        }
    }

    void MoveTime()
    {
        
        // I have the velocity of the stars along with the time by which I wanna move[need to figure out how we will control the time that we want to move] The code on 
        //chatgpt was the keyboard event I have to figure out how to make this work on menu
        // I will use transform to change the position of the star using the 
    }

    void CreateContellations(TextAsset constFile)
    {
        if (constellations != null)
        {
            //Debug.Log("Hello World");
            foreach (GameObject constellation in constellations)
            {
                constellation.SetActive(false);
            }
        }
        string allData2 = constFile.text;
        string[] lineData2 = allData2.Split('\n');
        GameObject mainConst = transform.gameObject;
        
        for (var i = 0; i < lineData2.Length; i++)
        {
            string[] eachLine2 = lineData2[i].Split(' ');
            var constellation = new GameObject();
            constellation.name = eachLine2[0];
            constellation.transform.parent = mainConst.transform;



            int k = 0;
            for (int j = 3; j < eachLine2.Length - 1; j += 2)
            {

                var line1 = Instantiate(linePrefab, constellation.transform);
                var line1Renderer = line1.GetComponent<LineRenderer>();
                line1Renderer.positionCount = 2;
                constellations.Add(line1);

                Vector3 position1 = starData[float.Parse(eachLine2[j].Trim())].position;


                Vector3 position2 = starData[float.Parse(eachLine2[j + 1].Trim())].position;

                
                line1Renderer.SetPosition(0, position1);
                line1Renderer.SetPosition(1, position2);
                k += 2;
            }
        }


    }
    void DrawConstellations(GameObject star1, GameObject star2)
    {
        
        GameObject line = Instantiate(linePrefab);
        var lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, star1.transform.position);
        lineRenderer.SetPosition(1, star2.transform.position);
        line.transform.SetParent(star1.transform, false);
        constellations.Add(line);
        line.SetActive(true);
    }
}
