using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInit : MonoBehaviour
{
    // Get material

    public Material material;

    void Start()
    {
        applyMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void applyMaterial()
    {
        // Get all renderers
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        //Apply material to itself
        GetComponent<Renderer>().material = material;

        // Apply material to all renderers
        foreach (Renderer renderer in renderers)
        {
            renderer.material = material;
        }
    }
}
