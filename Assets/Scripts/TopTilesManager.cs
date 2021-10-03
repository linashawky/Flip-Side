using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopTilesManager : MonoBehaviour
{
    public GameObject[] topTilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 7;
    public Transform playerTransfrom;
    private List<GameObject> activeTopTiles = new List<GameObject>();

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(topTilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTopTiles.Add(go);
        zSpawn += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTopTiles[0]);
        activeTopTiles.RemoveAt(0);
    }

    void Start()
    {
        Debug.Log(topTilePrefabs.Length);
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
            {
                if (i == 1)
                    SpawnTile(1);

                else
                    SpawnTile(UnityEngine.Random.Range(0, topTilePrefabs.Length));
            }
        }

    }

    void Update()
    {
        if (playerTransfrom.position.z - 35 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(UnityEngine.Random.Range(0, topTilePrefabs.Length));
            DeleteTile();
        }
    }
}
