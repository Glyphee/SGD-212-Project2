using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Controller : MonoBehaviour
{
    [SerializeField] private GameObject playerGO;
    [SerializeField] private GameObject projectileGO;
    [SerializeField] private float detectRadius;
    [SerializeField] private int throwPower;
    [SerializeField] GameObject deathDust;
    public int health;
    NavMeshAgent nav;
    AudioManager audioMan;
    private GameObject thisTemp;
    public Animator enemyAnimator;
    public DoorScript doorScript;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        audioMan = GetComponent<AudioManager>();
        StartCoroutine(DetectPlayer());
        StartCoroutine(ShootPlayer());
    }

    private void SelfDeath()
    {
        //Debug.Log("Starting Langsat Death");
        if(thisTemp != null) // detaches spike slime from enemy
        {
            thisTemp.GetComponent<SlimeController>().currEnemy = null;
            thisTemp.GetComponent<SlimeController>().isAttacking = false;
            thisTemp.GetComponent<SlimeController>().nav.enabled = true;
        }
        doorScript.deadEnemies--;
        print("Enemies left: " + doorScript.deadEnemies);
        if(doorScript.deadEnemies == 0)
        {
            MessageScript.J.StartMessage("That seems to be the last of them! Let's go see if the door is unjammed now.");
        }
        GameObject dust = Instantiate(deathDust) as GameObject;
        dust.transform.position = transform.position;
        Destroy(this.gameObject);
    }

    IEnumerator DetectPlayer()
    {
        while (true)
        {
            while (Vector3.Distance(playerGO.transform.position, transform.position) < detectRadius)
            {
                // Instantiate(projectileGO, new Vector3(0, 0, 0), Quaternion.identity);
                // yield return new WaitForSeconds(3f);

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

    IEnumerator ShootPlayer()
    {
        while (true)
        {
            while (Vector3.Distance(playerGO.transform.position, transform.position) < detectRadius)
            {
                GameObject projectile;
                projectile = Instantiate(projectileGO, transform.position + new Vector3(0, 0.5f, 0), transform.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 500 * throwPower);
                audioMan.Play("Attack");
                yield return new WaitForSeconds(3f);
            }
            while (Vector3.Distance(playerGO.transform.position, transform.position) >= detectRadius)
            {
                yield return null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int randSound = Random.Range(0, 2); //random sfx for getting hit by slimes
        if (other.gameObject.tag == "absorb" || other.gameObject.tag == "crush")
        {
            if (randSound == 0)
            {
                audioMan.Play("Hurt 1");
            }
            else
            {
                audioMan.Play("Hurt 2");
            }

            SelfDeath();
        }
        else if(other.gameObject.tag == "spike") // allows the spike slime to latch onto the Langsat
        {
            // Debug.Log("Langsat hit by SpikebSlime");
            thisTemp = other.gameObject;
            this.gameObject.GetComponent<NavMeshAgent>().speed = 0.5f;
        }
        // else if(other.gameObject.tag == "Player")
        // {
        //     StartCoroutine(IsAttacking());
        // }
    }

    // private IEnumerator IsAttacking()
    // {
    //     nav.speed = 0;
    //     enemyAnimator.SetTrigger("attack");
    //     yield return new WaitForSeconds(1);
    //     nav.speed = 2;
    // }
}
