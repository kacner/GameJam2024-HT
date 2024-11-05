using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float Distance;
    private GameObject player;
    private bool isAttached = false;
    private DistanceJoint2D distanceJoint;
    private LineRenderer linerenderer;
    void Start()
    {
        linerenderer = GetComponent<LineRenderer>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < 2.5f && !isAttached)
        {
            Attach();
        }


        if (isAttached)
        {
            linerenderer.SetPosition(0, transform.position); // Current object position
            linerenderer.SetPosition(1, player.transform.position); // Player position
        }
    }

    private void Attach()
    {
        isAttached = true;
        distanceJoint = gameObject.AddComponent<DistanceJoint2D>();
        distanceJoint.connectedBody = player.GetComponent<PlayerMovement>().rb;
        linerenderer.positionCount = 2;
    }
}
