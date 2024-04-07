using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExoColor : MonoBehaviour
{
    StarDataLoader instanceval;
    public TextAsset exo_data;
    public bool show_exo = false;
    Dictionary<string, int> spectral_types = new Dictionary<string, int>() { { "O", 0 }, { "B", 1 }, { "A", 2 }, { "F", 3 }, { "G", 4 }, { "K", 5 }, { "M", 6 } };
    // Start is called before the first frame update
    void Start()
    {
        instanceval = FindObjectOfType<StarDataLoader>();
    }

    public void originalColor()
    {
        
        foreach (KeyValuePair<float, GameObject> starRet in instanceval.starobjects)
        {
            string colorval = instanceval.starData[starRet.Key].color;
            if (spectral_types.ContainsKey(colorval))
            {
                if (colorval == 'O'.ToString())
                {
                    Color blue = new Color(0f, 0f, 1f, 1f);
                    starRet.Value.GetComponent<Renderer>().material.SetColor("_TintColor", blue);
                }

                else if (colorval == 'B'.ToString())
                {
                    Color lightBlue = new Color(0.678f, 0.847f, 0.902f, 1f);

                    starRet.Value.GetComponent<Renderer>().material.SetColor("_TintColor", lightBlue);
                }

                else if (colorval == 'A'.ToString())
                {

                }

                else if (colorval == 'F'.ToString())
                {
                    Color yellowWhite = new Color(1f, 1f, 0.878f, 1f);
                    starRet.Value.GetComponent<Renderer>().material.SetColor("_TintColor", yellowWhite);
                }

                else if (colorval == 'G'.ToString())
                {
                    Color yellow = new Color(1f, 1f, 0f, 1f);
                    starRet.Value.GetComponent<Renderer>().material.SetColor("_TintColor", yellow);
                }

                else if (colorval == 'K'.ToString())
                {
                    Color orange = new Color(1f, 0.647f, 0f, 1f);
                    starRet.Value.GetComponent<Renderer>().material.SetColor("_TintColor", orange);
                }

                else if (colorval == 'M'.ToString())
                {
                    Color red = new Color(1f, 0f, 0f, 1f);
                    starRet.Value.GetComponent<Renderer>().material.SetColor("_TintColor", red);
                }
            }
        }
    }

    public void show_exoData()
    {
        Debug.Log("Entered exo function");
        string allData3 = exo_data.text;
        string[] lineData3 = allData3.Split('\n');

        foreach (GameObject star in instanceval.starobjects.Values)
        {
            star.GetComponent<Renderer>().material.SetColor("_TintColor", Color.grey);
        }

        for (var i = 1; i < lineData3.Length - 1; i++)
        {
            string[] eachline3 = lineData3[i].Split(',');
            //string hip_number = eachline3[1].Trim().ToString();
            float.TryParse(eachline3[1], out float hip_number);
            if (hip_number.ToString() == "99699" || hip_number.ToString() == "60648" || hip_number.ToString() == "61820" || hip_number.ToString() == "19207" || hip_number.ToString() == "116454" || hip_number.ToString() == "65" || hip_number.ToString() == "71135" || hip_number.ToString() == "79431" || hip_number.ToString() == "14101" || hip_number.ToString() == "70705")
            {
                continue;
            }
            Debug.Log(hip_number);
            string colorval = eachline3[2].Trim().ToString();
            if (colorval != null)
            {
                if (colorval == '1'.ToString())
                {
                    instanceval.starobjects[hip_number].GetComponent<Renderer>().material.SetColor("_TintColor", Color.red);
                }

                else if (colorval == '2'.ToString())
                {
                    instanceval.starobjects[hip_number].GetComponent<Renderer>().material.SetColor("_TintColor", Color.yellow);
                }

                else if (colorval == '3'.ToString())
                {
                    instanceval.starobjects[hip_number].GetComponent<Renderer>().material.SetColor("_TintColor", Color.green);
                }

                else if (colorval == '4'.ToString())
                {
                    instanceval.starobjects[hip_number].GetComponent<Renderer>().material.SetColor("_TintColor", Color.cyan);
                }

                else if (colorval == '5'.ToString())
                {
                    instanceval.starobjects[hip_number].GetComponent<Renderer>().material.SetColor("_TintColor", Color.magenta);
                }

                else if (colorval == '6'.ToString())
                {
                    instanceval.starobjects[hip_number].GetComponent<Renderer>().material.SetColor("_TintColor", Color.blue);
                }


            }

            else
            {
                instanceval.starobjects[hip_number].GetComponent<Renderer>().material.SetColor("_TintColor", Color.white);

            }
        }
    }

    public void ToggleColor()
    {
        show_exo = !show_exo;
        if(show_exo)
        {
            show_exoData();
        }
        else
        {
            originalColor();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
