using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class StarGenerator : MonoBehaviour
{
    public string filePath = "Assets\\starlist.csv";

    // Start is called before the first frame update
    List<StarValues> values;
    public GameObject starPrefab;
    public GameObject namedStarPrefab;
    public GameObject namedObjects;
    public GameObject unnamedObjects;
    public GameObject starFinder;
    public bool includeLineRendering;
    private int numberOfStars;
    private bool globalSettingUpdated;
    private int numberOfUpdatedStars;

    //Global Settings
    [SerializeField]
    private int distanceLimit;
    [SerializeField]
    private float sizeScalar;
    [SerializeField]
    private bool adjustColorBasedOnSpectra;
    [SerializeField]
    private bool adjustColorBasedOnRedBlueShift;
    [SerializeField]
    private bool redBlueShiftBasedOnCamera;//default reverts to Sol
    [SerializeField]
    private bool scaleStars;
    [SerializeField]
    private bool scaleByAbsoluteMagnitude;
    [SerializeField]
    private bool adjustBrightness;
    [SerializeField]
    private bool showDataText;
    [SerializeField]
    private bool showNameOnly;
    [SerializeField]
    private bool showVelocityVectors;
    [SerializeField]
    private bool showConstellationLines;
    [SerializeField]
    private bool showNamelessStars;
    [SerializeField]
    private bool showClusterColors;

    public bool GlobalSettingUpdated()
    {
        return globalSettingUpdated;
    }
    private void resetUpdateTrigger()
    {
        globalSettingUpdated = true; numberOfUpdatedStars = 0;
    }
    public void reportUpdatedStar()
    {
        numberOfUpdatedStars++;
        if (numberOfUpdatedStars >= numberOfStars)
        {
            globalSettingUpdated = false;
            //OnVariableChange(globalSettingUpdated);
        }
    }
    //public delegate void OnVariableChangeDelegate(bool newVal);
    //public event OnVariableChangeDelegate OnVariableChange;


    //Galactic Redraw Required
    public int DistanceLimit { get => distanceLimit; set { distanceLimit = value; redrawGalaxy(); }}
    public bool ShowNamelessStars { get => showNamelessStars; set { showNamelessStars = value; redrawGalaxy(); } }

    //Simple Display Update Required
    public float SizeScalar { get => sizeScalar; set { sizeScalar = value; resetUpdateTrigger(); } }
    public bool AdjustColorBasedOnSpectra { get => adjustColorBasedOnSpectra; set { adjustColorBasedOnSpectra = value; resetUpdateTrigger(); } }
    public bool AdjustColorBasedOnRedBlueShift { get => adjustColorBasedOnRedBlueShift; set { adjustColorBasedOnRedBlueShift = value; resetUpdateTrigger(); } }
    public bool RedBlueShiftBasedOnCamera { get => redBlueShiftBasedOnCamera; set { redBlueShiftBasedOnCamera = value; resetUpdateTrigger(); } }
    public bool ScaleStars { get => scaleStars; set { scaleStars = value; resetUpdateTrigger(); } }
    public bool ScaleByAbsoluteMagnitude { get => scaleByAbsoluteMagnitude; set { scaleByAbsoluteMagnitude = value; resetUpdateTrigger(); } }
    public bool AdjustBrightness { get => adjustBrightness; set { adjustBrightness = value; resetUpdateTrigger(); } }
    public bool ShowDataText { get => showDataText; set { showDataText = value; resetUpdateTrigger(); } }
    public bool ShowNameOnly { get => showNameOnly; set { showNameOnly = value; resetUpdateTrigger(); } }
    public bool ShowVelocityVectors { get => showVelocityVectors; set { showVelocityVectors = value; resetUpdateTrigger(); } }
    public bool ShowConstellationLines { get => showConstellationLines; set { showConstellationLines = value; resetUpdateTrigger(); } }

    public bool ShowClusterColors { get => showClusterColors; set => showClusterColors = value; }

    void Start()
    {
        redrawGalaxy();
    }

    


    private void CronosDevours(GameObject Target) {
        foreach (Transform child in Target.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    private void redrawGalaxy() {
        CronosDevours(namedObjects);
        CronosDevours(unnamedObjects);

        if (showNamelessStars)
        {
            values = File.ReadAllLines(filePath)
                                           .Skip(1)
                                           .Select(v => StarValues.FromCsv(v))
                                           .Where(v => v.dist < DistanceLimit)
                                           .OrderBy(v => v.dist)
                                           .ToList();
        }
        else
        {
            values = File.ReadAllLines(filePath)
                                           .Skip(1)
                                           .Select(v => StarValues.FromCsv(v))
                                           .Where(v => v.dist < DistanceLimit && v.proper.Length > 0)
                                           .OrderBy(v => v.dist)
                                           .ToList();
        }
        

        numberOfStars = values.Count;

        Debug.Log("Count of stars = " + numberOfStars);

        for (int i = 0; i < values.Count; i++)
        {
            //Transform t = transform.Find(values[i].con + "(Clone)");
            //if (t == null)
            //{
            //    GameObject constellation = GameObject.Instantiate(new GameObject(values[i].con), transform);
            //    t = constellation.transform;
            //}

            if (values[i].proper.Length > 0)
            {
                GameObject g = GameObject.Instantiate(namedStarPrefab, new Vector3(values[i].x, values[i].y, values[i].z), Quaternion.identity, namedObjects.transform);
                g.GetComponent<StarData>().starGen = this;
                //if (i > 0)
                //{
                //    g.GetComponent<StarData>().constellationLineRenderer.SetPosition(0, new Vector3(values[i].x, values[i].y, values[i].z));
                //    g.GetComponent<StarData>().constellationLineRenderer.SetPosition(1, new Vector3(values[i].x, values[i].y, values[i].z));
                //}
                //else
                //{
                //    if (includeLineRendering) { GameObject.Instantiate(starFinder, g.transform); }
                    
                //}
                
                g.name = values[i].proper;
                g.GetComponent<StarData>().StarName = values[i].proper;
                g.GetComponent<StarData>().StarNumber = values[i].hip.ToString();
                g.GetComponent<StarData>().StarSpectra = values[i].spect;
                g.GetComponent<StarData>().StarConstellation = values[i].con;
                g.GetComponent<StarData>().StarAbsoluteMagnitude = values[i].absmag;
                g.GetComponent<StarData>().StarMagnitude = values[i].mag;
                g.GetComponent<StarData>().StarLuminosity = values[i].lum;
                g.GetComponent<StarData>().StarVelocity = new Vector3(values[i].vx, values[i].vy, values[i].vz);
                g.GetComponent<StarData>().clusterIndex = values[i].cluster;
            }
            else
            {
                GameObject g = GameObject.Instantiate(starPrefab, new Vector3(values[i].x, values[i].y, values[i].z), Quaternion.identity, unnamedObjects.transform);
                g.GetComponent<StarData>().starGen = this;
                //if (i > 0)
                //{
                //    g.GetComponent<StarData>().constellationLineRenderer.SetPosition(1, new Vector3(values[i - 1].x, values[i - 1].y, values[i - 1].z));
                //}
                //else
                //{
                //    if (includeLineRendering) { GameObject.Instantiate(starFinder, g.transform); }
                //}

                g.name = values[i].con + "_" + values[i].id.ToString();
                g.GetComponent<StarData>().StarNumber = values[i].hip.ToString();
                g.GetComponent<StarData>().StarSpectra = values[i].spect;
                g.GetComponent<StarData>().StarConstellation = values[i].con;
                g.GetComponent<StarData>().StarAbsoluteMagnitude = values[i].absmag;
                g.GetComponent<StarData>().StarMagnitude = values[i].mag;
                g.GetComponent<StarData>().StarLuminosity = values[i].lum;
                g.GetComponent<StarData>().StarVelocity = new Vector3(values[i].vx, values[i].vy, values[i].vz);
                g.GetComponent<StarData>().clusterIndex = values[i].cluster;
            }
        }
        unnamedObjects.SetActive(showNamelessStars);
    }

    class StarValues
    {
        public int id;
        public int hip;
        public int hd;
        public int hr;
        public string gl;
        public string bf;
        public string proper;
        public float ra;
        public float dec;
        public float dist;
        public float pmra;
        public float pmdec;
        public float rv;
        public float mag;
        public float absmag;
        public string spect;
        public float ci;
        public float x;
        public float y;
        public float z;
        public float vx;
        public float vy;
        public float vz;
        public float rarad;
        public float decrad;
        public float pmrarad;
        public float pmdecrad;
        public string bayer;
        public float flam;
        public string con;
        public float comp;
        public float comp_primary;
        public float base_val;
        public float lum;
        public string var;
        public float var_min;
        public float var_max;
        public int cluster;

        public static int intNullValueCheck(string value)
        {
            int outValue;
            bool success = int.TryParse(value, out outValue);
            return success ? outValue : 0;
        }
        public static float floatNullValueCheck(string value)
        {
            float outValue;
            bool success = float.TryParse(value, out outValue);
            return success ? outValue : 0.0f;
        }
        public static StarValues FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            StarValues starValues = new StarValues();
            
            starValues.id = intNullValueCheck(values[0]);
            starValues.hip = intNullValueCheck(values[1]);
            starValues.hd = intNullValueCheck(values[2]);
            starValues.hr = intNullValueCheck(values[3]);
            starValues.gl = Convert.ToString(values[4]);
            starValues.bf = Convert.ToString(values[5]);
            starValues.proper = Convert.ToString(values[6]);
            starValues.ra = floatNullValueCheck(values[7]);
            starValues.dec = floatNullValueCheck(values[8]);
            starValues.dist = floatNullValueCheck(values[9]);
            starValues.pmra = floatNullValueCheck(values[10]);
            starValues.pmdec = floatNullValueCheck(values[11]);
            starValues.rv = floatNullValueCheck(values[12]);
            starValues.mag = floatNullValueCheck(values[13]);
            starValues.absmag = floatNullValueCheck(values[14]);
            starValues.spect = Convert.ToString(values[15]);
            starValues.ci = floatNullValueCheck(values[16]);
            starValues.x = floatNullValueCheck(values[17]);
            starValues.y = floatNullValueCheck(values[18]);
            starValues.z = floatNullValueCheck(values[19]);
            starValues.vx = floatNullValueCheck(values[20]);
            starValues.vy = floatNullValueCheck(values[21]);
            starValues.vz = floatNullValueCheck(values[22]);
            starValues.rarad = floatNullValueCheck(values[23]);
            starValues.decrad = floatNullValueCheck(values[24]);
            starValues.pmrarad = floatNullValueCheck(values[25]);
            starValues.pmdecrad = floatNullValueCheck(values[26]);
            starValues.bayer = Convert.ToString(values[27]);
            starValues.flam = floatNullValueCheck(values[28]);
            starValues.con = Convert.ToString(values[29]);
            starValues.comp = floatNullValueCheck(values[30]);
            starValues.comp_primary = floatNullValueCheck(values[31]);
            starValues.base_val = floatNullValueCheck(values[32]);
            starValues.lum = floatNullValueCheck(values[33]);
            starValues.var = Convert.ToString(values[34]);
            starValues.var_min = floatNullValueCheck(values[35]);
            starValues.var_max = floatNullValueCheck(values[36]);
            starValues.hr = intNullValueCheck(values[37]);
            return starValues;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
