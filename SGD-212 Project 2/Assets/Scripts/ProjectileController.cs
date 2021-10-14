using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float createTime;
    private float projectileForce = 10f;
    Rigidbody projRB;
    void Start()
    {
        createTime = Time.time;
        projRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // projRB.AddForce(Vector3.forward* projectileForce);
    }

    void Update()
    {
        if ((Time.time - createTime) > 3f)
        {
            Destroy(this.gameObject);
        }
    }
}
