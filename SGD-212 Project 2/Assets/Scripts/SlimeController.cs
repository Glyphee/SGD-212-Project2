using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonoBehaviour
{
    [SerializeField] private GameObject playerGO;
    private bool hasStopped = false;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude < 1)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().transform.rotation = Quaternion.identity;
            hasStopped = true;
        }

        if (Vector3.Distance(transform.position, playerGO.transform.position) < 1 && hasStopped == true)
        {
            GetComponent<NavMeshAgent>().enabled = true;
            hasStopped = false;
            //playerGO.GetComponent<Playercontroller>().slimesHeld++;
        }
    }
}
