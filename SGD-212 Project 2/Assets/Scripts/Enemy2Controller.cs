using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2Controller : MonoBehaviour
{
    [SerializeField] private GameObject playerGO;
    [SerializeField] private float detectRadius;
    private int health = 2;
    NavMeshAgent nav;
    AudioManager audioMan;
    private GameObject thisTemp;
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
            thisTemp.GetComponent<SlimeController>().currEnemy = null;
            thisTemp.GetComponent<SlimeController>().isAttacking = false;
            thisTemp.GetComponent<SlimeController>().nav.enabled = true;
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
                if (playIdleSound)
                {
                    audioMan.Play("Idle");
                    playIdleSound = false;
                }
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
            PlayHurtSFX();
        }
        else if (other.gameObject.tag == "spike")
        {
            this.gameObject.GetComponent<NavMeshAgent>().speed = 0.5f;
            thisTemp = other.gameObject;
            PlayHurtSFX();
        }
        else if (other.gameObject.tag == "crush")
        {
            //Crush slime damage
            health--;
            health--;
            health--;
            PlayHurtSFX();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "absorb" || collision.gameObject.tag == "spike" || collision.gameObject.tag == "crush")
        {
            
        }
             
    }

    void PlayHurtSFX()
    {
        int randSound = Random.Range(0, 2); //random sfx for getting hit by slimes
        if (randSound == 0)
        {
            audioMan.Play("Hurt");
        }
        else
        {
            audioMan.Play("Hurt2");
        }
    }

}