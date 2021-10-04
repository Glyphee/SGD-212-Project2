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

    private void Update()
    {
        if (Vector3.Distance(playerGO.transform.position, transform.position) < detectRadius)
        {
            GetComponent<NavMeshAgent>().destination = playerGO.transform.position;
            StartCoroutine(ShootPlayer());
        }
        else
        {
            GetComponent<NavMeshAgent>().destination = transform.position;
            StopCoroutine(ShootPlayer());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "absorb")
        {
            //Absorb slime damage
        }
        else if (collision.gameObject.tag == "spike")
        {
            //Spike slime damage
        }
        else if (collision.gameObject.tag == "crush")
        {
            //Crush slime damage
        }
    }

    IEnumerator ShootPlayer()
    {
        GameObject clone;
        clone = Instantiate(projectileGO, transform.position, transform.rotation);
        clone.GetComponent<Rigidbody>().AddForce(Vector3.forward * throwPower);
        yield return new WaitForSeconds(3.0f);
    }
}
