using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeController : MonoBehaviour
{
    [SerializeField] private GameObject targetGO;
    [SerializeField] private GameObject normSlime;
    [SerializeField] private GameObject spikeSlime;
    [SerializeField] private GameObject expandSlime;
    public string slimeType = null;

    // Update is called once per frame
    void Update()
    {
        //Makes the slime follow the target
        GetComponent<NavMeshAgent>().destination = targetGO.transform.position;

        //Sets the slime to the correct model for the place it is in
        switch (slimeType)
        {
            case "norm":
                normSlime.SetActive(true);
                spikeSlime.SetActive(false);
                expandSlime.SetActive(false);
                print("slime 0 is normal");
                break;
            case "spike":
                normSlime.SetActive(false);
                spikeSlime.SetActive(true);
                expandSlime.SetActive(false);
                break;
            case "expand":
                normSlime.SetActive(false);
                spikeSlime.SetActive(false);
                expandSlime.SetActive(true);
                break;
            case null:
                normSlime.SetActive(false);
                spikeSlime.SetActive(false);
                expandSlime.SetActive(false);
                break;
        }
    }
}
