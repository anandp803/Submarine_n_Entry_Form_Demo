using UnityEngine;
using System.IO;
using TMPro;
using Unity.Mathematics;

public class ConfigLoader : MonoBehaviour
{
    [SerializeField]
    private TextAsset ConfigjsonFile; 
    public GameObject nightVisoncameraPrefab;
    public GameObject depthSensorPrefab;
    public GameObject cameraPrefabParent;
    public GameObject depthSensorPrefabParent;
    private GameObject Init_nightVisionObj;
    private Controller controller;

    void Awake()
    {
        controller = GetComponent<Controller>();
        if (ConfigjsonFile != null)
        {
            LoadConfig();
        }
        else
        {
            Debug.LogError("JSON file reference is missing!");
        }        
    }

    void LoadConfig()
    {
        if (ConfigjsonFile != null)
        {
            string json = ConfigjsonFile.text;
            SubmarineConfig config = JsonUtility.FromJson<SubmarineConfig>(json);
            Debug.Log("config loaded: " + config.submarine.camera_position +" "+ config.submarine.camera_rotation +" "+ config.submarine.depth_sensor_position);

            // Set camera and depth sensor positions from config
            Init_nightVisionObj= Instantiate(nightVisoncameraPrefab, config.submarine.camera_position, Quaternion.Euler(config.submarine.camera_rotation));
            Init_nightVisionObj.transform.parent = cameraPrefabParent.transform;
            Init_nightVisionObj.transform.localPosition = config.submarine.camera_position;
            Init_nightVisionObj.gameObject.SetActive(false);
            GameObject depthSensorObj= Instantiate(depthSensorPrefab, config.submarine.depth_sensor_position, quaternion.Euler(config.submarine.depth_sensor_rotation));
            depthSensorObj.transform.parent = depthSensorPrefabParent.transform;
            depthSensorObj.transform.localPosition = config.submarine.depth_sensor_position;
            controller.depthSensor = depthSensorObj.GetComponent<depthSensor>();
        }
        else
        {
            Debug.LogError("Config file not found!");
        }
    }

    public void ToggleNightVision(bool isOn)
    {
        Init_nightVisionObj.SetActive(isOn);
    }
    
}

[System.Serializable]
    public class SubmarineConfig
    {
        public SubmarineSettings submarine;

        [System.Serializable]
        public class SubmarineSettings
        {
            public Vector3 camera_position;
            public Vector3 camera_rotation;
            public Vector3 depth_sensor_position;
            public Vector3 depth_sensor_rotation;
        }
    }
