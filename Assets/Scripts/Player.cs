using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private bool hasKey;
    private GameManager gm;
    private GameObject key;
    public Image keyImage;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        key = GameObject.FindGameObjectWithTag("Key");
        keyImage.gameObject.SetActive(false);
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
            keyImage.gameObject.SetActive(true);
            var audio = GetComponent<AudioSource>();
            audio.time = 0.5f;
            audio.Play();
        }
    }
}
