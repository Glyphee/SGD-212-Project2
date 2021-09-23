using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonoBehaviour
{
    [SerializeField] private GameObject targetGO;

    // Update is called once per frame
    void Update()
    {
        //Makes the slime follow the target
        GetComponent<NavMeshAgent>().destination = targetGO.transform.position;
    }
}
