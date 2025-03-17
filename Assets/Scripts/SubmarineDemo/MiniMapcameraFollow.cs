using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapcameraFollow : MonoBehaviour
{
    public Camera miniMapCamera;  
    public Transform submarineTransform;

    void Start()
    {
        miniMapCamera = GetComponent<Camera>();
    }

    void Update()
    {
        // Follow the submarine
        miniMapCamera.transform.position = new Vector3(submarineTransform.position.x, miniMapCamera.transform.position.y, submarineTransform.position.z);
        miniMapCamera.transform.rotation = Quaternion.Euler(90f, submarineTransform.eulerAngles.y, 0f); 
    }

}
