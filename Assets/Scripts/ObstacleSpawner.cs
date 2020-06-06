using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    
    public spawnZone zone;
    public Vector2 spawnDirection;
    public ParticleSystem particle;

    float lastTimeSpawn;
    float spawnRate;

    [System.Serializable]
    public class spawnZone
    {
        public float width;    // scale in y axis
        public float length;   // scale in x axis
    }


   enum ObjectType{
        Meteor1,
        Meteor2,
        Wreck1,
        Wreck2
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        spawnRate = Random.Range(2, 5);
        if (lastTimeSpawn + spawnRate <= Time.time)
        {
            lastTimeSpawn = Time.time;
            SpawnObstacle();
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3(zone.length, zone.width, 0);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }

    void SpawnObstacle()
    {
        // Get randome type of obstacle
        string type = "" + (ObjectType)Random.Range(0, 4);

        // Random a position in spawnZone
        float spawnPosX = transform.position.x + Random.Range(-zone.length / 2, zone.length / 2);
        float spawnPosY = transform.position.y + Random.Range(-zone.width / 2, zone.width / 2);
        Vector3 randomPos = new Vector3(spawnPosX, spawnPosY, 0);

        // Random a rotation in z axis
        Quaternion rotation = Random.rotation;
        rotation.x = 0;
        rotation.y = 0;

        // Get the obstacle from pool and store in a varible to get Rigidbody
        ObjectPooler.Instance.SpawnFromPool(type, randomPos, rotation, spawnDirection, particle);
    }
}
