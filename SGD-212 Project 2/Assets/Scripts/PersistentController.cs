using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentController : MonoBehaviour
{
    public static PersistentController instance;
    public int absorbCount = 0;
    [SerializeField] private GameObject absorbSlime;
    public int spikeCount = 0;
    [SerializeField] private GameObject spikeSlime;
    public int crushCount = 0;
    [SerializeField] private GameObject crushSlime;
    public bool levelChanged = false;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject); //Keeps this object persistent throughout the scene changes
    }

    public void LateUpdate()
    {
        if (levelChanged == true)
        {
            print("accessed");
            levelChanged = false;
            while (absorbCount != 0)
            {
                absorbCount--;
                print("spawned absorb slime");
                // Instantiate(absorbSlime, transform);
                Instantiate(absorbSlime, transform);
            }
            while (spikeCount != 0)
            {
                spikeCount--;
                Instantiate(spikeSlime, transform);
            }
            while (crushCount != 0)
            {
                crushCount--;
                Instantiate(crushSlime, transform);
            }
        }
    }
}
