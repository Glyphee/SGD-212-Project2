using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2Controller : MonoBehaviour
{
    [SerializeField] private GameObject playerGO;
    [SerializeField] private float detectRadius;
    [SerializeField] GameObject deathDust;
    private int health = 2;
    NavMeshAgent nav;
    AudioManager audioMan;
    private GameObject thisTemp;
    public Animator enemyAnimator;
    public DoorScript doorScript;
    bool playIdleSound = false;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        audioMan = GetComponent<AudioManager>();
        StartCoroutine(DetectPlayer());
    }

    private void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(SelfDeath());
        }
    }

    private IEnumerator SelfDeath()
    {
        Debug.Log("Starting Zombie Death");       
        nav.speed = 0;
        if(thisTemp != null)
        {
            thisTemp.GetComponent<SlimeController>().currEnemy = null;
            thisTemp.GetComponent<SlimeController>().isAttacking = false;
            thisTemp.GetComponent<SlimeController>().nav.enabled = true;
            thisTemp.GetComponent<Collider>().enabled = true;
        }
        enemyAnimator.SetTrigger("Dead");
        this.gameObject.GetComponent<Collider>().enabled = false;       
        nav.isStopped = true;
        
        yield return new WaitForSeconds(2);
        doorScript.deadEnemies--;
        print("Enemies left: " + doorScript.deadEnemies);
        GameObject dust = Instantiate(deathDust) as GameObject;
        dust.transform.position = transform.position;
        dust.transform.localScale = new Vector3(3, 3, 3);
        Destroy(this.gameObject);
    }

    IEnumerator DetectPlayer()
    {
        while (true)
        {
            while (Vector3.Distance(playerGO.transform.position, transform.position) < detectRadius)
            {
                nav.destination = playerGO.transform.position;
                if (playIdleSound)
                {
                    audioMan.Play("Idle");
                    playIdleSound = false;
                }
                yield return null;
            }
            while (Vector3.Distance(playerGO.transform.position, transform.position) >= detectRadius)
            {
                nav.destination = transform.position;
                yield return null;
                playIdleSound = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "absorb")
        {
            //Absorb slime damage
            health--;
            
        }
        else if (other.gameObject.tag == "spike")
        {
            this.gameObject.GetComponent<NavMeshAgent>().speed = 0.5f;
            other.gameObject.GetComponent<Collider>().enabled = false;
            thisTemp = other.gameObject;
        }
        else if (other.gameObject.tag == "crush")
        {
            //Crush slime damage
            health = 0;
        }
        else if(other.gameObject.tag == "Player")
        {
            StartCoroutine(IsAttacking());
        }
    }

    private IEnumerator IsAttacking()
    {
        nav.speed = 0;
        enemyAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(1);
        nav.speed = 2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "absorb" || collision.gameObject.tag == "spike" || collision.gameObject.tag == "crush")
        {
            if(health > 0)
            {
                int randSound = Random.Range(0, 2); //random sfx for getting hit by slimes
                if (randSound == 0)
                {
                    audioMan.Play("Hurt 1");
                }
                else
                {
                    audioMan.Play("Hurt 2");
                }
            }
            else
            {
                audioMan.Play("Death");
            }
        }
    }

}