using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("KillBullet");
    }

    void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        Destroy(this.gameObject);
    }

    IEnumerator KillBullet()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
