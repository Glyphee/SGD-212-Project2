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
    public bool isAttacking = false; //if the slime is stuck to an enemy and attacking it
    public bool willPersist = false; //if the slime will persist through scene changes
    public GameObject currEnemy; //the enemy that the slime is attacking
    AudioManager audioMan;


    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        nav.enabled = false;
        controller = FindObjectOfType<Playercontroller>();
        audioMan = GetComponent<AudioManager>();
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
        
        if (Vector3.Distance(transform.position, playerGO.transform.position) < 1 && hasStopped == true && isAttacking == false)
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
                audioMan.Play("Pickup");
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
        if (nav.enabled == true && isAttacking == false)
        {
            nav.destination = playerGO.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!isInParty)
        {
            audioMan.Play("Hit SFX");
        }
        
        if (collision.collider.CompareTag("enemy2") || collision.collider.CompareTag("enemy3"))
        {
            isAttacking = true;
            currEnemy = collision.gameObject;
        }
    }
}
