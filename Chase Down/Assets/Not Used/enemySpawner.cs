using UnityEngine;
using System.Collections;
using UnityEngine.AI;


public class enemySpawner : MonoBehaviour
{
    public Terrain terrain;
    public int enemyCount; // number of objects to place
    int i = 0; // number of placed objects
    public GameObject theEnemy; // GameObject to place
    

    private int terrainWidth; // terrain size (x)
    private int terrainLength; // terrain size (z)
    private int terrainPosX; // terrain position x
    private int terrainPosZ; // terrain position z

    Vector3 spawnLoc;



    void Start()
    {
        
        // terrain size x
        terrainWidth = (int)terrain.terrainData.size.x;

        // terrain size z
        terrainLength = (int)terrain.terrainData.size.z;

        // terrain x position
        terrainPosX = (int)terrain.transform.position.x;

        // terrain z position
        terrainPosZ = (int)terrain.transform.position.z;
    }



    // Update is called once per frame
    void Update()
    {
        // generate objects
        if (i <= enemyCount)
        {
            // generate random x position
            int xPos = Random.Range(terrainPosX, terrainPosX + terrainWidth);

            // generate random z position
            int zPos = Random.Range(terrainPosZ, terrainPosZ + terrainLength);

            // get the terrain height at the random position
            float yPos = Terrain.activeTerrain.SampleHeight(new Vector3(xPos, 0, zPos));

            spawnLoc = new Vector3(xPos, yPos, zPos);

        

            // create new gameObject on random position
            GameObject newEnemy = (GameObject)Instantiate(theEnemy, spawnLoc, Quaternion.identity);

            newEnemy.GetComponent<NavMeshAgent>().Warp(spawnLoc);

            i += 1;
        }

        if (i == enemyCount)
        {
            Debug.Log("Generate objects complete!");
        }
    }
}