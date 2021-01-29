using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var spawners = GameObject.FindGameObjectsWithTag("Spawner");
        if (spawners.Length > 0)
        {
            var idx = Random.Range(0, spawners.Length);
            spawners[idx].GetComponent<Spawner>().SpawnKey();
        }
    }
}
