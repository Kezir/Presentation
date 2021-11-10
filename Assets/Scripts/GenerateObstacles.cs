using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour
{
    public Vector3 posToSpawn;
    private Vector3 addToPos;
    public Obstacle[] obstacles;
    private int randomObstacle;
    private int randomPlace;

    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            SpawnObstacle();
        }
       
        InvokeRepeating("SpawnObstacle", 0f, 1f);
    }


    private void SpawnObstacle()
    {
        randomObstacle = Random.Range(0, obstacles.Length);
        randomPlace = Random.Range(0, 3);

        if(obstacles[randomObstacle].size > 1)
        {
            if(Random.Range(0,2) == 0)
            {
                addToPos += Vector3.left * 2f;
            }
            else
            {
                addToPos -= Vector3.left * 2f;
            }
            
        }
        else
        {
            if (randomPlace == 0)
            {
                addToPos += Vector3.left * 2f;
            }
            else if(randomPlace == 1)
            {
                addToPos -= Vector3.left * 2f;
            }
        }
        Instantiate(obstacles[randomObstacle].prefab, posToSpawn + addToPos, Quaternion.identity);
        posToSpawn += Vector3.forward * 10;
        addToPos = Vector3.zero;
    }
}
