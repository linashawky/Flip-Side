using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomTilesManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 7;
    public Transform playerTransfrom;
    private List<GameObject> activeTiles = new List<GameObject>();

    public void SpawnTile(int tileIndex)
    {
       GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    void OnCollisionEnter(Collision otherObj)
    {
        if (otherObj.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject, .5f);
        }
    }

    void Start()
    {
        Debug.Log(tilePrefabs.Length);
        for(int i = 0; i < numberOfTiles; i++ )
        {
            if (i == 0)
                SpawnTile(0);
            else
            {
                if (i == 1)
                    SpawnTile(1);

                else
                    SpawnTile(UnityEngine.Random.Range(0, tilePrefabs.Length));
            }
        }

    }

    void Update()
    {
        if(playerTransfrom.position.z - 35 > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(UnityEngine.Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }
}
