using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance {get; private set;}
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int playerHealth = 100;
    public int enemy1Health = 100;
    public int enemy2Health = 100;
    public int enemy3Health = 100;
    public int enemy4Health = 100;
}
