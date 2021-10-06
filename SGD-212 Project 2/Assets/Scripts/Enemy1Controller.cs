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
    NavMeshAgent nav;
    AudioManager audioMan;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        audioMan = GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (Vector3.Distance(playerGO.transform.position, transform.position) < detectRadius)
        {
            nav.destination = playerGO.transform.position;
            StartCoroutine(ShootPlayer());
        }
        else
        {
            nav.destination = transform.position;
            StopCoroutine(ShootPlayer());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        int randSound = Random.Range(0, 2); //random sfx for getting hit by slimes
        if (collision.gameObject.tag == "absorb")
        {
            //Absorb slime damage
            if (randSound == 0)
            {
                audioMan.Play("Hurt 1");
            }
            else
            {
                audioMan.Play("Hurt 2");
            }
        }
        else if (collision.gameObject.tag == "spike")
        {
            //Spike slime damage
            if (randSound == 0)
            {
                audioMan.Play("Hurt 1");
            }
            else
            {
                audioMan.Play("Hurt 2");
            }
        }
        else if (collision.gameObject.tag == "crush")
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
        }
    }

    IEnumerator ShootPlayer()
    {
        GameObject clone;
        clone = Instantiate(projectileGO, transform.position, transform.rotation);
        clone.GetComponent<Rigidbody>().AddForce(Vector3.forward * throwPower);
        audioMan.Play("Attack");
        yield return new WaitForSeconds(3.0f);
    }
}
