using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

namespace StarMap
{
    public class StarDataFileReader : MonoBehaviour
    {

        public string filePath = "Assets\\starlist.csv";
        List<StarValue> values;
        public StarDisplaySettings starDisplaySettings;
        public int numberOfStars;
        public AudioSource backgroundMusic;

        private void Start()
        {
            if (PlayerPrefs.GetString("DisplaySettings") != "")
            {
                JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("DisplaySettings"), starDisplaySettings);
            }
            /*if (starDisplaySettings.PlayMusic)
            {
                backgroundMusic.enabled = true;
                backgroundMusic.Play();
            }
            else
            {
                backgroundMusic.enabled = false;
                backgroundMusic.Stop();
            }*/
        }

        public List<StarValue> readStarDataFile()
        {
            if (starDisplaySettings.ShowNamelessStars)
            {
                values = File.ReadAllLines(filePath)
                                               .Skip(1)
                                               .Select(v => FromCsv(v))
                                               .Where(v => v.dist < starDisplaySettings.DistanceLimit)
                                               .OrderBy(v => v.dist)
                                               .ToList();
            }
            else
            {
                values = File.ReadAllLines(filePath)
                                               .Skip(1)
                                               .Select(v => FromCsv(v))
                                               .Where(v => v.dist < starDisplaySettings.DistanceLimit && v.proper.Length > 0)
                                               .OrderBy(v => v.dist)
                                               .ToList();
            }

            numberOfStars = values.Count;

            Debug.Log("Count of stars = " + numberOfStars);
            return values;
        }





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
        public static StarValue FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            StarValue starValue = new StarValue();

            starValue.id = intNullValueCheck(values[0]);
            starValue.hip = intNullValueCheck(values[1]);
            starValue.hd = intNullValueCheck(values[2]);
            starValue.hr = intNullValueCheck(values[3]);
            starValue.gl = Convert.ToString(values[4]);
            starValue.bf = Convert.ToString(values[5]);
            starValue.proper = Convert.ToString(values[6]);
            starValue.ra = floatNullValueCheck(values[7]);
            starValue.dec = floatNullValueCheck(values[8]);
            starValue.dist = floatNullValueCheck(values[9]);
            starValue.pmra = floatNullValueCheck(values[10]);
            starValue.pmdec = floatNullValueCheck(values[11]);
            starValue.rv = floatNullValueCheck(values[12]);
            starValue.mag = floatNullValueCheck(values[13]);
            starValue.absmag = floatNullValueCheck(values[14]);
            starValue.spect = Convert.ToString(values[15]);
            starValue.ci = floatNullValueCheck(values[16]);
            starValue.x = floatNullValueCheck(values[17]);
            starValue.y = floatNullValueCheck(values[18]);
            starValue.z = floatNullValueCheck(values[19]);
            starValue.vx = floatNullValueCheck(values[20]);
            starValue.vy = floatNullValueCheck(values[21]);
            starValue.vz = floatNullValueCheck(values[22]);
            starValue.rarad = floatNullValueCheck(values[23]);
            starValue.decrad = floatNullValueCheck(values[24]);
            starValue.pmrarad = floatNullValueCheck(values[25]);
            starValue.pmdecrad = floatNullValueCheck(values[26]);
            starValue.bayer = Convert.ToString(values[27]);
            starValue.flam = floatNullValueCheck(values[28]);
            starValue.con = Convert.ToString(values[29]);
            starValue.comp = floatNullValueCheck(values[30]);
            starValue.comp_primary = floatNullValueCheck(values[31]);
            starValue.base_val = floatNullValueCheck(values[32]);
            starValue.lum = floatNullValueCheck(values[33]);
            starValue.var = Convert.ToString(values[34]);
            starValue.var_min = floatNullValueCheck(values[35]);
            starValue.var_max = floatNullValueCheck(values[36]);
            starValue.hr = intNullValueCheck(values[37]);
            return starValue;
        }
    }
}