using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemyController : MonoBehaviour
{
    public int enemyHealth = 100;

    public GameObject bulletGO;
    public GameObject gunTip;
    public GameObject gunParent;
    public GameObject playerGO;

    bool isShooting = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }

        gunParent.transform.rotation = Quaternion.Slerp(gunParent.transform.rotation, Quaternion.LookRotation(playerGO.transform.position - transform.position), Time.deltaTime * 100f);

        if (Vector3.Distance(transform.position, playerGO.transform.position) < 10 && isShooting == false)
        {
            isShooting = true;
            InvokeRepeating("ShootPlayer", 0f, 1f);
        }
        else if (Vector3.Distance(transform.position, playerGO.transform.position) > 10)
        {
            isShooting = false;
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
