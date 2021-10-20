using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy3Controller : MonoBehaviour
{
    [SerializeField] private GameObject playerGO;
    [SerializeField] private float detectRadius;
    [SerializeField] GameObject deathDust;
    private int health = 3;
    NavMeshAgent nav;
    AudioManager audioMan;
    private GameObject thisTemp;
    public Animator enemyAnimator;

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
        Debug.Log("Starting Skeleton Death");
        nav.speed = 0;
        if(thisTemp != null)
        {
            thisTemp.GetComponent<SlimeController>().currEnemy = null;
            thisTemp.GetComponent<SlimeController>().isAttacking = false;
            thisTemp.GetComponent<SlimeController>().nav.enabled = true;
            thisTemp.GetComponent<Collider>().enabled = true;
        }
        enemyAnimator.SetTrigger("Dead_1");
        this.gameObject.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(2);
        DoorScript.D.deadBosses++;
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
                yield return null;
            }
            while (Vector3.Distance(playerGO.transform.position, transform.position) >= detectRadius)
            {
                nav.destination = transform.position;
                yield return null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "crush")
        {
            health = 0;
        }
        else if(other.gameObject.tag == "absorb")
        {
            
            health--;
        }
        else if(other.gameObject.tag == "spike")
        {
            thisTemp = other.gameObject;
            this.gameObject.GetComponent<NavMeshAgent>().speed = 0.5f;
            other.gameObject.GetComponent<Collider>().enabled = false;
        }
        else if(other.gameObject.tag == "Player")
        {
            StartCoroutine(IsAttacking());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "absorb" || collision.gameObject.tag == "spike" || collision.gameObject.tag == "crush")
        {
            if (health > 0)
            {
                audioMan.Play("Hurt");
            }
            else
            {
                audioMan.Play("Death");
            }
        }
    }

    private IEnumerator IsAttacking()
    {
        audioMan.Play("Attack");
        nav.speed = 0;
        enemyAnimator.SetTrigger("Strike_1");       
        yield return new WaitForSeconds(1);
        nav.speed = 2;
    }
}