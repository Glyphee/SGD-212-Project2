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

    public Animator SlimeAnim;


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
                //print("Slimes in party: " + controller.slimesInParty);
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

        if (isAttacking)
        {
            transform.position = currEnemy.transform.position + new Vector3(0,0.5f,0);
            nav.enabled = false;
        }

        //Sets slime's destination to the player (Moved from player controller)
        if (nav.enabled == true && isAttacking == false)
        {
            nav.destination = playerGO.transform.position;

            SlimeAnim.SetTrigger("Walk");
        }
    }

    private void OnTriggerEnter(Collider collider)
    {        
        
        if (collider.CompareTag("enemy2") && this.gameObject.CompareTag("spike") || collider.CompareTag("enemy3") && this.gameObject.CompareTag("spike"))
        {
            isAttacking = true;
            currEnemy = collider.gameObject;

            SlimeAnim.SetTrigger("Attack");
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("enemy2") || collider.CompareTag("enemy3"))
        {
            isAttacking = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isInParty)
        {
            audioMan.Play("Hit SFX");
        }
    }
}
