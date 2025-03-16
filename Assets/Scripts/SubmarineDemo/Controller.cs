using TMPro;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject submarine; 
    public GameObject normalCamera; 
    public ConfigLoader configLoader; 
    public bool isNightVisionOn = false;
    public TMP_Text currentDepthText;
    public depthSensor depthSensor;

    void Update()
    {        
        if (depthSensor != null && currentDepthText != null)
        {
            currentDepthText.text = "Depth: " + depthSensor.currentDepth.ToString("F2") + " m";
        }
        else
        {
            Debug.LogWarning("Depth sensor or text reference is not set.");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (configLoader != null)
            {
                isNightVisionOn = !isNightVisionOn;
                configLoader.ToggleNightVision(isNightVisionOn);
                normalCamera.transform.GetChild(0).gameObject.SetActive(!isNightVisionOn);
            }
            else
            {
                Debug.LogWarning("ConfigLoader reference is not set.");
            }
        }
    }
}
