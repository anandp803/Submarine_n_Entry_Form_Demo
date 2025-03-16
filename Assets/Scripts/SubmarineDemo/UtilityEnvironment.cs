using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this class is used to generate prefabs on a plane before runtime
/// </summary>
public class UtilityEnvironment : MonoBehaviour
{
    public GameObject[] prefabs; 
    public int gridRows = 10; 
    public int gridColumns = 10; 
    public float prefabSpacing = 10f; 
    public float scale = 1f; 
    public GameObject plane; 

    [ContextMenu("Generate Prefabs")]
    void GeneratePrefabsFromInspector()
    {        
        if (plane != null)
        {            
            GeneratePrefabsOnPlane();
        }
        else
        {
            Debug.LogError("Plane reference is not assigned!");
        }
    }

/// <summary>
/// this method is used to generate prefabs on a plane
/// </summary>
    void GeneratePrefabsOnPlane()
    {       
        float planeWidth = plane.transform.localScale.x * 10f; 
        float planeLength = plane.transform.localScale.z * 10f; 
       
        float startX = -planeWidth / 2 + prefabSpacing / 2;
        float startZ = -planeLength / 2 + prefabSpacing / 2;
       
        for (int row = 0; row < gridRows; row++)
        {
            for (int col = 0; col < gridColumns; col++)
            {                
                float xPosition = startX + col * prefabSpacing;
                float zPosition = startZ + row * prefabSpacing;
               
                GameObject selectedPrefab = prefabs[Random.Range(0, prefabs.Length)];
                
                Vector3 position = new Vector3(xPosition, 0f, zPosition);
                GameObject instance= Instantiate(selectedPrefab, position, Quaternion.identity);
                instance.transform.parent = plane.transform; 
                instance.transform.localScale = new Vector3(scale, scale, scale); 
            }
        }
    }

}
