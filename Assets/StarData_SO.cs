using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarData_SO : ScriptableObject
{
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
}
