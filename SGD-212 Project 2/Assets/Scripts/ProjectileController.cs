using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float createTime;
    void Start()
    {
        createTime = Time.time;
    }

    void Update()
    {
        if ((Time.time - createTime) > 3f)
        {
            Destroy(this.gameObject);
        }
    }
}
