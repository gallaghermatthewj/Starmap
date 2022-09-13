using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;


public class StarEntity_ECS 
{
    public struct StarDataBuild
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
}
