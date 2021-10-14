using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy3Controller : MonoBehaviour
{
    [SerializeField] private GameObject playerGO;
    [SerializeField] private float detectRadius;
    private int health = 3;
    NavMeshAgent nav;
    AudioManager audioMan;
    private GameObject thisTemp;

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
            thisTemp.GetComponent<SlimeController>().currEnemy = null;
            thisTemp.GetComponent<SlimeController>().isAttacking = false;
            thisTemp.GetComponent<SlimeController>().nav.enabled = true;
            DoorScript.D.deadBosses++;
            Destroy(this.gameObject);
        }
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
        int randSound = Random.Range(0, 2); //random sfx for getting hit by slimes
        if (other.gameObject.tag == "crush")
        {
            //Crush slime damage
            if (randSound == 0)
            {
                audioMan.Play("Hurt 1");
            }
            else
            {
                audioMan.Play("Hurt 2");
            }
            health--;
            health--;
            health--;
        }
        else if(other.gameObject.tag == "absorb")
        {
            if (randSound == 0)
            {
                audioMan.Play("Hurt 1");
            }
            else
            {
                audioMan.Play("Hurt 2");
            }
            health--;
        }
        else if(other.gameObject.tag == "spike")
        {
            thisTemp = other.gameObject;
            this.gameObject.GetComponent<NavMeshAgent>().speed = 0.5f;
        }
    }
}