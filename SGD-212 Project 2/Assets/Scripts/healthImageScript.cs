using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthImageScript : MonoBehaviour
{
    public static healthImageScript instance;

    public Image[] healthImages;
    private Image currentImage;

    public int playerHealth = 3;
    public Playercontroller playercontroller;

    void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentImage = healthImages[3];
        Debug.Log("Current health image " + currentImage);
        print("Starting Health: " + playerHealth);
    }

    void Update()
    {
        CheckHealth();
    }

    public void ChangeHealthImage(int temp)
    {
        currentImage.gameObject.SetActive(false);
        healthImages[temp].gameObject.SetActive(true);
        currentImage = healthImages[temp];
        Debug.Log("Current health image " + currentImage);
    }

    public void CheckHealth()
    {
        if(playerHealth > -1)
        {
            ChangeHealthImage(playerHealth);
        }
        else if(playerHealth <= 0)
        {
            ChangeHealthImage(0);
            Debug.Log("Starting GameOver");
            StartCoroutine(playercontroller.GameOver());
        }
    }
}
