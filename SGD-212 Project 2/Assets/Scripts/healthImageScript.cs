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
        // Debug.Log("Current health image " + currentImage);
    }

    void Update()
    {
        CheckHealth();
    }

    public void ChangeHealthImage()
    {
        currentImage.gameObject.SetActive(false);
        healthImages[playerHealth].gameObject.SetActive(true);
        currentImage = healthImages[playerHealth];
        //Debug.Log("Current health image " + currentImage);
    }

    public void CheckHealth()
    {
        if(playerHealth <= 0)
        {
            ChangeHealthImage();
            Debug.Log("Starting GameOver");
            StartCoroutine(playercontroller.GameOver());
            this.gameObject.GetComponent<healthImageScript>().enabled = false;
        }
    }
}
