using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationCreator : MonoBehaviour
{
    StarDataLoader instanceval;
    public TextAsset[] constellationFiles;
    public GameObject linePrefab;
    public int selectedConst = -1;
    public List<LineRenderer> lineList = new List<LineRenderer>();
    public List<GameObject> constellations = new List<GameObject>();
    StarMove moveInst;
    void Start()
    {
        instanceval = FindObjectOfType<StarDataLoader>();
        moveInst = FindObjectOfType<StarMove>();
        InvokeRepeating("CheckRepeat", 0f, 0.5f);

    }

    public void ConstellationSelecter(int i)
    {
        
        selectedConst = i;
        CreateContellations(constellationFiles[i]);
    }
    void DestroyInstances()
    {
        foreach(LineRenderer lines in lineList)
        {
            Destroy(lines);
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
    void CreateContellations(TextAsset constFile)
    {
        if (constellations != null)
        {
            
            constellations = new List<GameObject>();
            DestroyInstances();
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
                LineRenderer line1Renderer = line1.GetComponent<LineRenderer>();
                line1Renderer.positionCount = 2;
                line1Renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                constellations.Add(line1);
                lineList.Add(line1Renderer);

                Vector3 position1 = instanceval.starobjects[float.Parse(eachLine2[j].Trim())].transform.position;
                
                Renderer starRenderer = instanceval.starobjects[float.Parse(eachLine2[j].Trim())].GetComponent<Renderer>();
                if (starRenderer != null)
                {
                    starRenderer.enabled = true;
                    instanceval.starobjects[float.Parse(eachLine2[j].Trim())].transform.LookAt(instanceval.cam.transform);
                }

                Vector3 position2 = instanceval.starobjects[float.Parse(eachLine2[j + 1].Trim())].transform.position;
                Renderer starRenderer2 = instanceval.starobjects[float.Parse(eachLine2[j + 1].Trim())].GetComponent<Renderer>();
                if (starRenderer2 != null)
                {
                    starRenderer2.enabled = true;
                    instanceval.starobjects[float.Parse(eachLine2[j+1].Trim())].transform.LookAt(instanceval.cam.transform);
                }

                line1Renderer.SetPosition(0, position1);
                line1Renderer.SetPosition(1, position2);
                k += 2;
            }
        }


    }

    void CheckRepeat()
    {
        if (moveInst.isTimeRunning)
        {
            ConstellationSelecter(selectedConst);
        }
    }    

    
}
