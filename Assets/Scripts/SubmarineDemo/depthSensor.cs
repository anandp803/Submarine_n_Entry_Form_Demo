using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class depthSensor : MonoBehaviour
{
    public float maxDepth = 500f; 
    public float currentDepth; 
    public LayerMask seaSurfaceLayer; 
     
    void Update()
    {
        
        Debug.DrawRay(transform.position, Vector3.down * maxDepth, Color.blue);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, maxDepth, seaSurfaceLayer))
        {
            currentDepth = hit.distance;                 
        }
        else
        {
            currentDepth = maxDepth;
        }    
    }

}
