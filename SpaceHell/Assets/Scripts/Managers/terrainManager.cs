using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    public GameObject terrainPrefab;
    public float terrainMoveSpeed = 2000f;
    public float terrainSpawnDistance = 200f;
    public float terrainDisappearDistance = 300f; // New variable for terrain disappearance distance

    private Transform playerTransform;
    private List<GameObject> terrainSegments = new List<GameObject>();

    void Start()
    {
        playerTransform = Camera.main.transform;
        InitializeTerrain();
    }

    void Update()
    {
        MoveTerrain();
        CheckForTerrainSpawn();
        CheckForTerrainDisappear();
    }

    void InitializeTerrain()
    {
        Vector3 initialPosition = new Vector3(-125f, -10f, -250f);
        GameObject initialTerrain = Instantiate(terrainPrefab, initialPosition, Quaternion.identity);
        terrainSegments.Add(initialTerrain);
    }

    void MoveTerrain()
    {
        foreach (GameObject terrainSegment in terrainSegments)
        {
            terrainSegment.transform.Translate(Vector3.back * terrainMoveSpeed * Time.deltaTime);
        }
    }

    void CheckForTerrainSpawn()
    {
        float distanceToLastTerrain = Vector3.Distance(playerTransform.position, terrainSegments[terrainSegments.Count - 1].transform.position);
        if (distanceToLastTerrain >= terrainSpawnDistance)
        {
            SpawnNewTerrain();
        }
    }

    void CheckForTerrainDisappear()
    {
        // Check if any terrain segment is beyond the disappearance distance and remove it
        for (int i = 0; i < terrainSegments.Count; i++)
        {
            float distanceToPlayer = Vector3.Distance(playerTransform.position, terrainSegments[i].transform.position);
            if (distanceToPlayer >= terrainDisappearDistance)
            {
                Destroy(terrainSegments[i]);
                terrainSegments.RemoveAt(i);
                i--; // Adjust index to account for the removed element
            }
        }
    }

    void SpawnNewTerrain()
    {
        Vector3 spawnPosition = terrainSegments[terrainSegments.Count - 1].transform.position + Vector3.forward * terrainSpawnDistance;
        GameObject newTerrain = Instantiate(terrainPrefab, spawnPosition, Quaternion.identity);
        terrainSegments.Add(newTerrain);
    }
}
