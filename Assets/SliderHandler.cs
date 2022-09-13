//Attach this script to a Toggle GameObject. To do this, go to Create>UI>Toggle.
//Set your own Text in the Inspector window

using UnityEngine;
using UnityEngine.UI;

public class SliderHandler : MonoBehaviour
{
    Slider m_Slider;
    public GlobalStarDisplaySettingsManager settingsManager;
    public string propertyToAffect;
    public GameObject[] gameObjects;
    public Text displayText;
    void Start()
    {
        //Fetch the Toggle GameObject
        m_Slider = GetComponent<Slider>();
        float value;
        if(propertyToAffect == "distance")
        {
            value = settingsManager.currentSettings.DistanceScalar;
        }
        else
        {
            value = settingsManager.currentSettings.SizeScalar;
        }
        m_Slider.value = value;
        displayText.text = value.ToString();
        //Add listener for when the state of the Slider changes, to take action
        m_Slider.onValueChanged.AddListener(delegate {
            SliderValueChanged(m_Slider);
        });
    }

    //Output the new state of the Slider
    void SliderValueChanged(Slider change)
    {
        displayText.text = m_Slider.value.ToString();
        switch (propertyToAffect)
        {
            case "distance":
                settingsManager.setDistanceScalar(m_Slider.value);
                for (int i = 0; i < gameObjects.Length; i++)
                {
                    float nScale = m_Slider.value;
                    gameObjects[i].transform.localScale = new Vector3(nScale, nScale, nScale);
                }
                
                break;
            case "scalar":
                settingsManager.setScalar(m_Slider.value);
                for(int i = 0; i < gameObjects.Length; i++)
                {
                    float nScale = 10 * m_Slider.value * 2.512f;
                    gameObjects[i].transform.localScale = new Vector3(nScale, nScale, nScale);
                }
                break;
            default:
                break;
        }
        
    }
}