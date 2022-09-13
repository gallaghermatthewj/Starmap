//Attach this script to a Toggle GameObject. To do this, go to Create>UI>Toggle.
//Set your own Text in the Inspector window

using UnityEngine;
using UnityEngine.UI;

public class ToggleHandler : MonoBehaviour
{
    Toggle m_Toggle;
    public GlobalStarDisplaySettingsManager settingsManager;
    public string propertyToAffect;

    void Start()
    {
        //Fetch the Toggle GameObject
        m_Toggle = GetComponent<Toggle>();
        if (propertyToAffect == "music")
        {
            //m_Toggle.isOn = settingsManager.currentSettings.PlayMusic;
        }
        else if (propertyToAffect == "constellations")
        {
            m_Toggle.isOn = settingsManager.currentSettings.ShowConstellationLines;
        }

        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });

    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change)
    {
        if(propertyToAffect == "music")
        {
            GameObject.Find("MainTheme").SetActive(m_Toggle.isOn);
        }
        else if(propertyToAffect == "constellations")
        {
            settingsManager.toggleConstellations(m_Toggle.isOn);
        }
        //settingsManager.toggleMusic(m_Toggle.isOn);
    }
}