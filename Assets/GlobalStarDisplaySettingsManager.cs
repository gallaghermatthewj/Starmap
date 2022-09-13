using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalStarDisplaySettingsManager : MonoBehaviour
{
    [SerializeField]
    public StarDisplaySettings currentSettings;
    private StarDisplaySettings tempStarDisplaySettings;
    public string sceneName;

    public string key;

    // Start is called before the first frame update
    private void OnEnable()
    {
        tempStarDisplaySettings = (StarDisplaySettings) ScriptableObject.CreateInstance(typeof(StarDisplaySettings));
        
        if (PlayerPrefs.GetString("DisplaySettings") != "")
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("DisplaySettings"), currentSettings);
        }
        cancelChanges();
    }

    public void syncSettings()
    {
        currentSettings = tempStarDisplaySettings;
        string jsonData = JsonUtility.ToJson(currentSettings, true);
        key = jsonData;
        PlayerPrefs.SetString("DisplaySettings", jsonData);
        PlayerPrefs.Save();
    }

    public void cancelChanges()
    {
        tempStarDisplaySettings.DistanceLimit = currentSettings.DistanceLimit;
        tempStarDisplaySettings.DistanceScalar = currentSettings.DistanceScalar;
        tempStarDisplaySettings.SizeScalar = currentSettings.SizeScalar;
        tempStarDisplaySettings.PlayMusic = currentSettings.PlayMusic;
    }

    public void setDistance(int newValue)
    {
        tempStarDisplaySettings.DistanceLimit = newValue;
    }

    public void setDistanceScalar(float newValue)
    {
        tempStarDisplaySettings.DistanceScalar = newValue;
    }

    public void toggleMusic(bool newValue)
    {
        tempStarDisplaySettings.PlayMusic = newValue;
    }

    public void toggleConstellations(bool newValue)
    {
        tempStarDisplaySettings.ShowConstellationLines = newValue;
    }

    public void setScalar(float newValue)
    {
        tempStarDisplaySettings.SizeScalar = newValue;
    }
    public void OpenMainScene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
