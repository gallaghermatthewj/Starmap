using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarData : MonoBehaviour
{
    private Color[] clusterColors = { Color.grey, Color.blue, Color.cyan, Color.green, Color.yellow, Color.magenta, Color.red, Color.white};

    private string starName;
    private string starNumber;
    private string starSpectra;
    private string starConstellation;
    private float starMagnitude;
    private float starAbsoluteMagnitude;
    private float starLuminosity;
    private Vector3 starVelocity;
    private Color colorToUse;
    private bool isRefreshing;
    public int clusterIndex;


    public GameObject textCanvas;
    public Text starNameT;
    public Text starNumberT;
    public Text starSpectraT;
    public Text starConstellationT;
    public Text starMagnitudeT;
    public Text starAbsoluteMagnitudeT;
    public Text starLuminosityT;
    public MeshRenderer meshRenderer;
    private Material material;
    public LineRenderer velocityLineRenderer;
    public LineRenderer constellationLineRenderer;
    public StarGenerator starGen;

    public Image clusterImage;
    


    public string StarName { get => starName; set { starName = value; starNameT.text = StarName; } }
    public string StarNumber { get => starNumber; set { starNumber = value; starNumberT.text = StarNumber; } }

    public string StarConstellation { get => starConstellation; set { starConstellation = value; starConstellationT.text = StarConstellation; } }
    public float StarMagnitude { get => starMagnitude; set { starMagnitude = value;
            if (starAbsoluteMagnitude < 0)
            {
                //transform.localScale /= Mathf.Abs(starMagnitude * 0.001f);
            }
            else
            {
                //transform.localScale *= (starMagnitude * 0.001f);
            }
            

            starMagnitudeT.text = StarMagnitude.ToString(); } }

    public float StarAbsoluteMagnitude { get => starAbsoluteMagnitude; set {
            starAbsoluteMagnitude = value;
            
            transform.localScale *= Mathf.Abs(starAbsoluteMagnitude > 0? starAbsoluteMagnitude * 2.512f : 1/(starAbsoluteMagnitude*2.512f)) * starGen.SizeScalar;
            starAbsoluteMagnitudeT.text = StarAbsoluteMagnitude.ToString(); } }

    public float StarLuminosity { get => starLuminosity;
        set {
            starLuminosity = value;
            float currentLuminosity = gameObject.GetComponent<MeshRenderer>().material.GetFloat("_Brightness");
            //gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Brightness", currentLuminosity * starLuminosity);
            starLuminosityT.text = StarLuminosity.ToString(); } }

    public Vector3 StarVelocity { get => starVelocity;
        set
        {
            starVelocity = value;
            Vector3[] lrArray = { new Vector3(0, 0, 0), starVelocity * 1000 };
            velocityLineRenderer.SetPositions(lrArray);
        }
    }

    //setStarSpectra
    public string StarSpectra
    {
        //  O   Blue-violet 	>30,000 K 	            0.00003% 	Stars of Orion's Belt
        //  B   Blue-white 	    10,000 K - 30,000 K 	0.13% 	    Rigel
        //  A   White 	        7,500 K - 10,000 K 	    0.6% 	    Sirius
        //  F   Yellow-white 	6,000 K - 7,500 K 	    3% 	        Polaris
        //  G   Yellow 	        5,000 K - 6,000 K 	    7.6% 	    Sun
        //  K   Orange 	        3,500 K - 5000 K 	    12.1% 	    Arcturus
        //  M   Red-orange      <3,500 K 	            76.5% 	    Proxima Centauri

        get => starSpectra;
        set
        {
            if (value.Length > 0)
            {
                starSpectra = value;

                switch (value.Substring(0, 1))
                {
                    case "O":
                        starSpectra = "O: Blue-Violet";
                        colorToUse = new Color(120, 125, 243, 255);
                        break;
                    case "B":
                        starSpectra = "B: Blue-White";
                        colorToUse = new Color(231, 240, 255, 255);
                        //gameObject.GetComponent<MeshRenderer>().material.color = new Color(231, 240, 255);
                        break;
                    case "A":
                        starSpectra = "A: White";
                        colorToUse = new Color(255, 255, 255, 255);
                        break;
                    case "F":
                        starSpectra = "F: Yellow-White";
                        colorToUse = new Color(255, 251, 195, 255);
                        break;
                    case "G":
                        starSpectra = "G: Yellow";
                        colorToUse = new Color(253, 216, 141, 255);
                        break;
                    case "K":
                        starSpectra = "K: Orange";
                        colorToUse = new Color(251, 197, 124, 255);
                        break;
                    case "M":
                        starSpectra = "M: Red-Orange";
                        colorToUse = new Color(160, 66, 41, 255);
                        break;
                    default:
                        starSpectra = "";
                        colorToUse = new Color(255, 198, 126, 255);
                        break;
                }
                colorToUse = shiftColor(colorToUse);
                if (starGen)
                {
                    if (starGen.AdjustColorBasedOnSpectra)
                    {
                        gameObject.GetComponent<MeshRenderer>().material.SetColor("_RimColor", colorToUse);
                    }
                }
                //gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Brightness", 0.5f);
                starSpectraT.text = starSpectra;
            }
            else
            {
                starSpectraT.text = "";
            }
        }
    }

    Color shiftColor(Color colorToShift)
    {
        Color shiftedColor = new Color();
        shiftedColor.r = colorToShift.r * 0.05f;
        shiftedColor.g = colorToShift.g * 0.05f;
        shiftedColor.b = colorToShift.b * 0.05f;
        shiftedColor.a = 255;
        return shiftedColor;
    }


    // Start is called before the first frame update
    void Start()
    {
        starGen = GameObject.Find("Logos").GetComponent<StarGenerator>();
        textCanvas.SetActive(starGen.ShowDataText);
        starNumberT.gameObject.SetActive(!starGen.ShowNameOnly);
        starSpectraT.gameObject.SetActive(!starGen.ShowNameOnly);
        starConstellationT.gameObject.SetActive(!starGen.ShowNameOnly);
        starMagnitudeT.gameObject.SetActive(!starGen.ShowNameOnly);
        starAbsoluteMagnitudeT.gameObject.SetActive(!starGen.ShowNameOnly);
        starLuminosityT.gameObject.SetActive(!starGen.ShowNameOnly);
        clusterImage.gameObject.SetActive(starGen.ShowClusterColors);
        if (starGen.ShowClusterColors)
        {
            clusterImage.color = clusterColors[clusterIndex - 1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if Global setting updated, update all stats
        //if (starGen.GlobalSettingUpdated() & !isRefreshing)
        //{
        //    IEnumerator coroutine = triggerStellarRefresh();
        //    StartCoroutine(coroutine);
        //}
    }

    IEnumerator triggerStellarRefresh()
    {
        isRefreshing = true;

        StarName = starName;
        StarNumber = starNumber;
        StarConstellation = starConstellation;
        StarMagnitude = starMagnitude;
        StarAbsoluteMagnitude = starAbsoluteMagnitude;
        StarLuminosity = starLuminosity;
        StarVelocity = starVelocity;
        StarSpectra = starSpectra;
        

        //Set Text
        textCanvas.SetActive(starGen.ShowDataText);
        starNumberT.gameObject.SetActive(!starGen.ShowNameOnly);
        starSpectraT.gameObject.SetActive(!starGen.ShowNameOnly);
        starConstellationT.gameObject.SetActive(!starGen.ShowNameOnly);
        starMagnitudeT.gameObject.SetActive(!starGen.ShowNameOnly);
        starAbsoluteMagnitudeT.gameObject.SetActive(!starGen.ShowNameOnly);
        starLuminosityT.gameObject.SetActive(!starGen.ShowNameOnly);

        starGen.reportUpdatedStar();


        var t = 0f;
        while (t < 2)
        {
            t += Time.deltaTime;
            
            yield return null;
        }
        isRefreshing = false;
    }


}
