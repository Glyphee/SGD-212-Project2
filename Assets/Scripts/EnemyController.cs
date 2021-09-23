using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    public GameObject point4;
    Vector3[] points = new Vector3[4];
    public int destPoint = 0;
    public float chaseDistance = 10f;

    public int enemyHealth = 100;

    public GameObject bulletGO;
    public GameObject gunTip;
    public GameObject gunParent;
    public GameObject playerGO;

    bool isShooting = false;

    void Start()
    {
        points[0] = point1.transform.position;
        points[1] = point2.transform.position;
        points[2] = point3.transform.position;
        points[3] = point4.transform.position;
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if(points.Length == 0) {return;}
        GetComponent<NavMeshAgent>().destination = points[destPoint];
        destPoint = (destPoint + 1) % points.Length;
    }

    void Update()
    {
        if (Vector3.Distance(playerGO.transform.position, transform.position) < chaseDistance) //Chasing
        {
            GetComponent<NavMeshAgent>().destination = playerGO.transform.position;
        }
        else if (!GetComponent<NavMeshAgent>().pathPending && GetComponent<NavMeshAgent>().remainingDistance < 0.5f) //Patrolling
        {
            GotoNextPoint();
        }

        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }

        gunParent.transform.rotation = Quaternion.Slerp(gunParent.transform.rotation, Quaternion.LookRotation(playerGO.transform.position - transform.position), Time.deltaTime * 100f);

        if (Vector3.Distance(transform.position, playerGO.transform.position) < 10 && isShooting == false)
        {
            isShooting = true;
            InvokeRepeating("ShootPlayer", 0f, 1f);
            GetComponent<NavMeshAgent>().stoppingDistance = 2;
        }
        else if (Vector3.Distance(transform.position, playerGO.transform.position) > 10)
        {
            isShooting = false;
            GetComponent<NavMeshAgent>().stoppingDistance = 1;
            CancelInvoke();
        }

    }

    void ShootPlayer()
    {
        GameObject firedBullet = Instantiate(bulletGO, gunTip.transform.position, gunParent.transform.rotation);
        firedBullet.GetComponent<Rigidbody>().velocity = gunTip.transform.forward * 10f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet1")
        {
            enemyHealth -= 10;
        }
        if (collision.gameObject.tag == "bullet2")
        {
            enemyHealth -= 25;
        }
    }
}
