using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopBehaviour : MonoBehaviour
{
    //public GameObject followLocation;

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
        //transform.position = Vector3.MoveTowards(transform.position, followLocation.transform.position, 1.0f);

        //Vector3 eulerRotation = new Vector3(transform.eulerAngles.x, followLocation.transform.eulerAngles.y, -transform.eulerAngles.z);
        //Vector3 eulerRotation = new Vector3(-transform.eulerAngles.x, followLocation.transform.eulerAngles.y, -transform.eulerAngles.z);
       // transform.rotation = Quaternion.Euler(eulerRotation);
    }

    void OnTriggerEnter(Collider other)   //void OnColliderEnter(Collision other)
    {
        //if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Sweep") || GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Slash"))
        //{
            GameObject obj = other.gameObject;

            if (!obj.CompareTag("Player") && obj.GetComponent<Rigidbody>() != null)
            {
                Vector3 dir = (transform.position - obj.transform.position).normalized;

                Rigidbody rb = obj.GetComponent<Rigidbody>();

                //add force pushing object away from player
                rb.AddForce(-dir * 50, ForceMode.Impulse);
            }

        //}
    }
}
