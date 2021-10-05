using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonoBehaviour
{
    [SerializeField] private GameObject playerGO;
    private bool hasStopped = false;
    NavMeshAgent nav;
    Rigidbody rb;
    Playercontroller controller;
    public bool isInParty = false; //if the slime is in the player's party
    public bool isHeld = false; //if the player is holding the slime


    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        nav.enabled = false;
        controller = FindObjectOfType<Playercontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude < 1)
        {
            rb.velocity = Vector3.zero;
            rb.transform.rotation = Quaternion.identity;
            hasStopped = true;
            
        }
        
        if (Vector3.Distance(transform.position, playerGO.transform.position) < 1 && hasStopped == true)
        { //player is close and is not moving
            nav.enabled = true;
            hasStopped = false;
            if(!isInParty)
            {
                isInParty = true;
                controller.slimesInParty++;
                print("Slimes in party: " + controller.slimesInParty);
                controller.slimeList.Add(this.gameObject);
            }
            if(!controller.isHolding)
            {
                isHeld = true;
            }
            
        }

        //not working as intended, TODO
        /*if (Vector3.Distance(transform.position, playerGO.transform.position) > 10 && isInParty && !isHeld)
        {
            rb.transform.position = playerGO.transform.position;
            print("Slime too far, moving to player");
            isInParty = false;
        }*/

        //Sets slime's destination to the player (Moved from player controller)
        if (nav.enabled == true)
        {
            nav.destination = playerGO.transform.position;
        }
    }
}
