using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Data",menuName ="ScriptableObjects/StarDisplaySettingsScriptableObject",order =1)]
public class StarDisplaySettings : ScriptableObject
{
    [SerializeField]
    private bool showNamelessStars = true;
    [SerializeField]
    private bool adjustColorBasedOnSpectra = true;
    [SerializeField]
    private bool adjustColorBasedOnRedBlueShift = false;
    [SerializeField]
    private bool redBlueShiftBasedOnCamera = false;
    [SerializeField]
    private bool scaleStars = true;
    [SerializeField]
    private bool scaleByAbsoluteMagnitude = true;
    [SerializeField]
    private bool adjustBrightness = true;
    [SerializeField]
    private bool showDataText = true;
    [SerializeField]
    private bool showNameOnly = true;
    [SerializeField]
    private bool showVelocityVectors;
    [SerializeField]
    private bool showConstellationLines;
    [SerializeField]
    private bool showClusterColors;
    [SerializeField]
    private bool useLODModel = true;
    [SerializeField]
    private bool favorQualityOverPerformance = true;
    [SerializeField]
    private bool playMusic = true;
    [SerializeField]
    private int distanceLimit = 10000;
    [SerializeField]
    private float sizeScalar = 0.0005f;
    [SerializeField]
    private float distanceScalar = 1.0f;

    public bool ShowNamelessStars { get => showNamelessStars; set => showNamelessStars = value; }
    public bool AdjustColorBasedOnSpectra { get => adjustColorBasedOnSpectra; set => adjustColorBasedOnSpectra = value; }
    public bool AdjustColorBasedOnRedBlueShift { get => adjustColorBasedOnRedBlueShift; set => adjustColorBasedOnRedBlueShift = value; }
    public bool RedBlueShiftBasedOnCamera { get => redBlueShiftBasedOnCamera; set => redBlueShiftBasedOnCamera = value; }
    public bool ScaleStars { get => scaleStars; set => scaleStars = value; }
    public bool ScaleByAbsoluteMagnitude { get => scaleByAbsoluteMagnitude; set => scaleByAbsoluteMagnitude = value; }
    public bool AdjustBrightness { get => adjustBrightness; set => adjustBrightness = value; }
    public bool ShowDataText { get => showDataText; set => showDataText = value; }
    public bool ShowNameOnly { get => showNameOnly; set => showNameOnly = value; }
    public bool ShowVelocityVectors { get => showVelocityVectors; set => showVelocityVectors = value; }
    public bool ShowConstellationLines { get => showConstellationLines; set => showConstellationLines = value; }
    public bool ShowClusterColors { get => showClusterColors; set => showClusterColors = value; }
    public bool UseLODModel { get => useLODModel; set => useLODModel = value; }
    public bool FavorQualityOverPerformance { get => favorQualityOverPerformance; set => favorQualityOverPerformance = value; }
    public bool PlayMusic { get => playMusic; set => playMusic = value; }
    public int DistanceLimit { get => distanceLimit; set => distanceLimit = value; }
    public float SizeScalar { get => sizeScalar; set => sizeScalar = value; }
    public float DistanceScalar { get => distanceScalar; set => distanceScalar = value; }


}
