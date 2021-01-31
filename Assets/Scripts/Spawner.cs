using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnList;
    public GameObject keyObj;
    public int objectsToSpawn = 10;
    public float spawnRadius = 12.5f;
    public float maxSpawnHeight = 10f;

    public void SpawnKey()
    {
        var posX = Random.Range(-spawnRadius, spawnRadius);
        var posZ = Random.Range(-spawnRadius, spawnRadius);
        Instantiate(keyObj, new Vector3(transform.position.x + posX, transform.position.y, transform.position.z + posZ), Quaternion.identity);
        var spawnIdx = Random.Range(0, spawnList.Length);
        var gm = Instantiate(spawnList[spawnIdx], new Vector3(transform.position.x + posX, transform.position.y + 0.25f, transform.position.z + posZ), Quaternion.Euler(new Vector3(0, 0, -90)));
    }

    public void SpawnObjects()
    {
        for (var i = 0; i < objectsToSpawn; ++i)
        {
            Debug.Log(i.ToString());
            var posX = Random.Range(-spawnRadius, spawnRadius);
            var posY = Random.Range(0f, maxSpawnHeight);
            var posZ = Random.Range(-spawnRadius, spawnRadius);
            var spawnIdx = Random.Range(0, spawnList.Length);
            var gm = Instantiate(spawnList[spawnIdx], new Vector3(transform.position.x + posX, transform.position.y + posY, transform.position.z + posZ), Random.rotation);
            var randomScale = Random.Range(0.5f, 1f);
            var ls = gm.transform.localScale;
            gm.transform.localScale = new Vector3(ls.x * randomScale, ls.y * randomScale, ls.z * randomScale);
        }
    }
}
