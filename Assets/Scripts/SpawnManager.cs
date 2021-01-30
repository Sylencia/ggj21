using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var spawners = GameObject.FindGameObjectsWithTag("Spawner");
        var idx = Random.Range(0, spawners.Length);
        for (var i = 0; i < spawners.Length; ++i)
        {
            if (i == idx)
            {
                spawners[i].GetComponent<Spawner>().SpawnKey();
            }
            spawners[i].GetComponent<Spawner>().SpawnObjects();
        }
    }
}
