using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopBehaviour : MonoBehaviour
{
    public GameObject followLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, followLocation.transform.position, 1.0f);
    }

    void OnCollisionEnter(Collision other)   //void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("RightSwing"))
        {
            GameObject obj = other.gameObject;

            if (!other.gameObject.CompareTag("Player"))
            {
                Vector3 dir = (transform.position - obj.transform.position).normalized;

                Rigidbody rb = obj.GetComponent<Rigidbody>();

                //add force pushing object away from player
                rb.AddForce(-dir * 100, ForceMode.Impulse);
            }

        }
    }
}
