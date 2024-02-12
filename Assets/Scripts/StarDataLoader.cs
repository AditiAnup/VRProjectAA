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
    //private Dictionary<string, StarData> 
    public bool show_exo = false;

    public Color colorArray;
    Dictionary<string, int> spectralTypes = new Dictionary<string, int>() { { "O", 0 }, { "B", 1 }, { "A", 2 }, { "F", 3 }, { "G", 4 }, { "K", 5 }, { "M", 6 } };
	
	public List<StarData> starData = new List<StarData>();

	void Start()
	{

		ParseFile();
        string[] constellationship = constellationsrc.text.Split('\n');
        for (int i = 0; i < 30; i++)
        {
            string[] const1 = constellationship[i].Split(new[] { " " }, StringSplitOptions.None);
            int.TryParse(const1[1], out int lines);
            string[] constellation = const1.Skip(3).ToArray();
            CreateContellations(lines, constellation);
        }
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

                float.TryParse(csvVal[6], out float scale);
                float.TryParse(csvVal[0], out float id);
                float.TryParse(csvVal[1], out float hip);
                StarData star = new StarData();
                star.size = star.GetSize(scale);
                star.position = new Vector3(x, y, z);
                float.TryParse(csvVal[5], out float absoluteMagnituge);
                star.absMag = absoluteMagnituge;
                star.id = id;
                star.hip = hip;

                starData.Add(star);
                var stars = Instantiate(starPrefab, star.position, Quaternion.LookRotation(star.position), gameObject.transform);
                stars.name = "S" + starCount;
                stars.transform.localScale = new Vector3(star.size, star.size, star.size);
                starCount++;
                //if(csvVal[11].Length != 0)
                //{
                //    stars.GetComponent<Renderer>().material.SetColor("_Color", colorArray[spectralTypes[csvVal[11].ToString()]]);
                //}
            }
            catch(IndexOutOfRangeException)
            {
                continue;
            }
            
		}
	}
    void CreateContellations(int lines, string[] const1)
    {
        
        List<StarData> stars = new List<StarData>();

        foreach (StarData eachstar in starData)
        {
            if(!float.IsNaN(eachstar.hip))
            {
                string hipstar = eachstar.hip.ToString();
                
                if(const1.Any(element => element.Trim() == hipstar.Trim()))
                {
                    stars.Add(eachstar);
                }
            }
        }
        GameObject constellationHolder = new GameObject("ConstellationHolder");
        constellationHolder.transform.parent = transform;
        for(int i = 0; i<const1.Length; i+=2)
        {
            int.TryParse(const1[i].Trim(), out int hip1);
            int.TryParse(const1[i+1].Trim(), out int hip2);
            var line = Instantiate(linePrefab, constellationHolder.transform);
            
            var lineRenderer = line.GetComponent<LineRenderer>();
            Vector3 pos1 = Vector3.zero;
            Vector3 pos2 = Vector3.zero;
            foreach(StarData estar in stars)
            {
                if(hip1 == estar.hip)
                {
                    pos1 = estar.position;
                }
                else if(hip2 == estar.hip)
                {
                    pos2 = estar.position;
                }
            }
            
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, pos1);
            lineRenderer.SetPosition(1, pos2);
        }
        
    }
}
