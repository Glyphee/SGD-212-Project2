using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2Controller : MonoBehaviour
{
    [SerializeField] private GameObject playerGO;
    [SerializeField] private float detectRadius;
    private int health = 3;
    NavMeshAgent nav;
    AudioManager audioMan;

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
        
        if (other.gameObject.tag == "absorb")
        {
            //Absorb slime damage
            HurtSFX();
            health--;
        }
        else if (other.gameObject.tag == "spike")
        {
            this.gameObject.GetComponent<NavMeshAgent>().speed = 0.5f;
        }
        else if (other.gameObject.tag == "crush")
        {
            //Crush slime damage
            HurtSFX();
            health--;
            health--;
            health--;
        }
    }

    void HurtSFX()
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
}