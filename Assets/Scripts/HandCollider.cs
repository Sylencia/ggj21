using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour
{
    private bool isColliding = false;
    private bool isVisible = false;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        isVisible = rend.isVisible;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hand")
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hand")
        {
            isColliding = false;
        }
    }

    public bool canGrab()
    {
        return gameObject.activeSelf && isColliding && isVisible;
    }
}
