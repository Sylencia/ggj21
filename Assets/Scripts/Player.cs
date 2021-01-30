using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool hasKey;
    private GameManager gm;
    private GameObject key;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        key = GameObject.FindGameObjectWithTag("Key");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish" && hasKey)
        {
            gm.CompleteGame();
        }
    }

    public void GrabItem()
    {
        if(!key)
        {
            key = GameObject.FindGameObjectWithTag("Key");
        }

        var hc = key.GetComponent<HandCollider>();
        
        if (hc.canGrab())
        {
            key.SetActive(false);
            hasKey = true;
        }
    }
}
