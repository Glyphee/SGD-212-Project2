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
    private int health = 1;
    NavMeshAgent nav;
    AudioManager audioMan;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        audioMan = GetComponent<AudioManager>();
        StartCoroutine(DetectPlayer());
        StartCoroutine(ShootPlayer());
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

    IEnumerator ShootPlayer()
    {
        while (true)
        {
            while (Vector3.Distance(playerGO.transform.position, transform.position) < detectRadius)
            {
                GameObject projectile;
                projectile = Instantiate(projectileGO, transform.position + new Vector3(0, .5f, 0), transform.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(transform.forward * 500 * throwPower);
                print("shooting");
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
            //Absorb slime damage
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
    }
}
