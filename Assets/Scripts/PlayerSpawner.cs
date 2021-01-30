using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSpawner : MonoBehaviour
{

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        int layer1 = LayerMask.NameToLayer("P1 cam");
        int layer2 = LayerMask.NameToLayer("P2 cam");

        player = Resources.Load("PlayerHolder") as GameObject;
        GameObject P1 = Instantiate(player) as GameObject;
        Camera c1 = P1.GetComponentInChildren<Camera>();
        c1.rect = new Rect(0, 0.5f, 1, 1);              //let this camera take top half (horizontal) of splitscreen

        CinemachineVirtualCamera v1 = P1.GetComponentInChildren<CinemachineVirtualCamera>();
        v1.gameObject.layer = layer1;

        int oldMask1 = c1.cullingMask;
        c1.cullingMask = oldMask1 & ~(1 << layer2);     //make layer2 invisible for this camera


        GameObject P2 = Instantiate(player) as GameObject;
        P2.transform.position = new Vector3(5, 2, 5);
        Camera c2 = P2.GetComponentInChildren<Camera>();
        c2.rect = new Rect(0, -0.5f, 1, 1);             //let this camera take bottom half of splitscreen

        CinemachineVirtualCamera v2 = P2.GetComponentInChildren<CinemachineVirtualCamera>();
        v2.gameObject.layer = layer2;

        int oldMask2 = c2.cullingMask;
        c2.cullingMask = oldMask2 & ~(1 << layer1);     //make layer1 invisible for this camera
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
